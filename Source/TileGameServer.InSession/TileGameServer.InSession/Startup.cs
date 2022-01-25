using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using TileGameServer.InSession.Constants;
using TileGameServer.InSession.DataAccess.Context;
using TileGameServer.InSession.Domain.Configuration;
using TileGameServer.InSession.Domain.Creators;
using TileGameServer.InSession.Domain.Library;
using TileGameServer.InSession.Hubs;
using WebApiBaseLibrary.Authorization.Configurators;
using WebApiBaseLibrary.Authorization.Constants;
using WebApiBaseLibrary.Authorization.Extensions;
using WebApiBaseLibrary.MessageQueueing.Configuration;
using WebApiBaseLibrary.MessageQueueing.Extensions.RabbitMQ;
using HeaderNames = Microsoft.Net.Http.Headers.HeaderNames;

namespace TileGameServer.InSession
{
    public class Startup
    {
        private IServiceProvider _serviceProvider;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var rabbitMqConfiguration = new RabbitMQConfiguration
            {
                HostName = Environment.GetEnvironmentVariable(EnvironmentVariables.RabbitMQHostName),
                Port = int.Parse(Environment.GetEnvironmentVariable(EnvironmentVariables.RabbitMQPort)!),
                VirtualHost = Environment.GetEnvironmentVariable(EnvironmentVariables.RabbitMQVirtualHost),
                UserName = Environment.GetEnvironmentVariable(EnvironmentVariables.RabbitMQUserName),
                Password = Environment.GetEnvironmentVariable(EnvironmentVariables.RabbitMQPassword)
            };

            services.AddMessageQueueingServices(typeof(Startup));
            services.AddRabbitMQ(rabbitMqConfiguration);

            services.AddSingletonJwtConfiguration(Configuration);
            services.AddSingleton<IJwtConfigurator, JwtConfigurator>();

            //services.AddScoped<IJwtReader, JwtReader>();

            var tileConfiguration = Configuration.GetSection("TileGameConfiguration").Get<TileGameConfiguration>();
            services.AddScoped<ITileLibrary, TileLibrary>(s => new TileLibrary(tileConfiguration));
            services.AddScoped<ITileFieldFactory, TileFieldFactory>();

            /*services.AddConfiguredJwtBearer(() =>
            {
                var jwtConfigurator = _serviceProvider.GetService<IJwtConfigurator>();

                return jwtConfigurator?.ValidationParameters;
            });*/

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var jwtConfigurator = _serviceProvider.GetService<IJwtConfigurator>();

                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = jwtConfigurator?.ValidationParameters;

                /*options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = a =>
                    {
                        Console.WriteLine(a.SecurityToken);
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        
                        context.Token = token;
                        
                        Console.WriteLine(token);

                        return Task.CompletedTask;
                    }
                };*/
            });
            services.AddAuthorization();

            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "TileGameServer", Version = "v1" });

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

            services.AddSingleton<IInSessionContext, LazyInSessionContext>();
            services.AddSignalR();
            services.AddMediatR(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _serviceProvider = app.ApplicationServices;

            app.UseMessageQueueingServices(typeof(Startup));

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<TileGameHub>("/TileGame");
                endpoints.MapControllers();
            });
        }
    }
}