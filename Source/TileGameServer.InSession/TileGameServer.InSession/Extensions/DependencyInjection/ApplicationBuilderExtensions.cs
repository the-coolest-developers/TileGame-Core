using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;
using TileGameServer.InSession.Attributes;
using TileGameServer.Reflection;
using WebApiBaseLibrary.Infrastructure.MessageQueueing;

namespace TileGameServer.InSession.Extensions.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMessageQueueingServices(this IApplicationBuilder app)
        {
            var messageQueueServices = ReflectionHelper.GetAllTypesWithAttribute<MessageQueueServiceAttribute>();

            var methods = messageQueueServices.Select(s => s.GetMethods());

            var methods = mqService.GetMethods();

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes<MessageQueueActionAttribute>(false).ToArray();
                if (attributes.Any())
                {
                    var connectionFactory = app.ApplicationServices.GetService<IMessageQueueConnectionFactory>();
                    if (connectionFactory != null)
                    {
                        var connection = connectionFactory.CreateConnection();

                        foreach (var attribute in attributes)
                        {
                            var reader = connection.CreateReader(attribute.QueueName);

                            var receiveActionParameter = method.GetParameters().First();

                            reader.SetReceivedAction(message =>
                            {
                                var serviceInstance = app.ApplicationServices.GetService<TMessageQueueingService>();

                                var parameterType = receiveActionParameter.ParameterType;
                                var deserializedParameter = JsonConvert.DeserializeObject(message, parameterType);

                                method.Invoke(serviceInstance, new[] { deserializedParameter });
                            });

                            reader.StartReading();
                        }
                    }
                }
            }

            return app;
        }
    }
}