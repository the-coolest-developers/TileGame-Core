using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using TileGameServer.BaseLibrary.DataAccess.Context;
using TileGameServer.BaseLibrary.DataAccess.Repositories;
using TileGameServer.Constants;
using TileGameServer.DataAccess.Repositories.GameSessions;
using TileGameServer.Domain.Configurators.SessionCapacityConfigurators;
using TileGameServer.Domain.Models.Configurations;
using TileGameServer.Extensions;
using TileGameServer.Infrastructure;
using TileGameServer.Infrastructure.MessageQueueing;
using TileGameServer.Infrastructure.MessageQueueing.RabbitMQ;
using WebApiBaseLibrary.Authorization.Configurators;
using WebApiBaseLibrary.Authorization.Constants;
using WebApiBaseLibrary.Authorization.Extensions;
using WebApiBaseLibrary.Authorization.Generators;
using WebApiBaseLibrary.Authorization.Models;
using HeaderNames = TileGameServer.Constants.HeaderNames;
using Schemes = TileGameServer.Constants.Schemes;

namespace TileGameServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private IServiceProvider _serviceProvider;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_ =>
            {
                var configuration = Configuration.GetSection("RabbitMQConfiguration").Get<RabbitMQConfiguration>();

                return configuration;
            });
            services.AddSingleton<IMessageQueueConnectionFactory, RabbitMQConnectionFactory>(_ =>
            {
                var configuration = _serviceProvider.GetService<RabbitMQConfiguration>();

                var connectionFactory = new RabbitMQConnectionFactory(configuration?.HostName);

                return connectionFactory;
            });
            services.AddScoped<IMessageQueuePublisher, RabbitMQPublisher>(_ =>
            {
                var connectionFactory = _serviceProvider.GetService<IMessageQueueConnectionFactory>();
                var connection = connectionFactory?.GetConnection();

                var publisher = connection?.CreatePublisher("JoinGameQueue");

                return (RabbitMQPublisher) publisher;
            });

            var databaseConnectionString = Configuration.GetConnectionString("PostgreSqlAws");

            services.AddSingleton(_ =>
            {
                var requestLimitConfiguration = Configuration
                    .GetSection(TileGameAppSettings.RequestLimitConfiguration)
                    .Get<RequestLimitConfiguration>();

                return requestLimitConfiguration;
            });

            services.AddDbContext<GameSessionContext>(options => options.UseNpgsql(databaseConnectionString));
            services.AddDbContext<PlayerContext>(options => options.UseNpgsql(databaseConnectionString));

            services.AddScoped<IGameSessionRepository, GameSessionDbRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();

            services.AddSingletonSessionCapacityConfiguration(Configuration);
            services.AddScoped<ISessionCapacityConfigurator, SessionCapacityConfigurator>();

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddSingleton<IJwtConfigurator, JwtConfigurator>(_ =>
            {
                var jwtConfiguration = Configuration.GetSection(AuthorizationAppsettings.JwtConfiguration)
                    .Get<JwtConfiguration>();

                return new JwtConfigurator(jwtConfiguration);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var jwtConfigurator = _serviceProvider.GetService<IJwtConfigurator>();

                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = jwtConfigurator?.ValidationParameters;
            });
            services.AddAuthorization();

            services.AddControllers().AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TileGameServer", Version = "v1"});
            });

            services.AddAuthorization(
                options => { options.AddRequireAdministratorRolePolicy(); });

            services.AddSwaggerGen(options =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "Json Web Token for authorization. Write: 'Bearer {your token}'",
                    Name = HeaderNames.Authorization,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = Schemes.Bearer
                };
                options.AddSecurityDefinition(securityScheme.Scheme, securityScheme);

                var requirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = securityScheme.Scheme
                            },
                            Scheme = Schemes.OAuth,
                            Name = securityScheme.Scheme,
                            In = securityScheme.In
                        },
                        new List<string>()
                    }
                };

                options.AddSecurityRequirement(requirement);
            });

            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _serviceProvider = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TileGameServer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}