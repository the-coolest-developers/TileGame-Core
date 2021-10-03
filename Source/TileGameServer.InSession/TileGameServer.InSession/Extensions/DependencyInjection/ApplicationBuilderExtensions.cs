using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using TileGameServer.InSession.Attributes;
using TileGameServer.InSession.Reflection;
using WebApiBaseLibrary.Infrastructure.MessageQueueing;

namespace TileGameServer.InSession.Extensions.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMessageQueueingServices(this IApplicationBuilder app)
        {
            var messageQueueServices = ReflectionHelper
                .GetAllTypesWithAttribute<MessageQueueServiceAttribute>()
                .ToArray();

            var connection = app.ApplicationServices.GetService<IMessageQueueConnection>();

            foreach (var messageQueueServiceType in messageQueueServices)
            {
                var methodsWithAttribute =  messageQueueServiceType.GetMethods()
                    .Where(m => m.GetCustomAttributes<MessageQueueActionAttribute>().Any());

                var messageQueueServiceActionMethods = methodsWithAttribute.ToDictionary(
                    m => m.GetCustomAttribute<MessageQueueActionAttribute>()?.QueueName,
                    m => m);

                foreach (var (queueName, method) in messageQueueServiceActionMethods)
                {
                    var reader = connection?.CreateReader(queueName);

                    var parameter = method.GetParameters().FirstOrDefault();
                    var parameterType = parameter?.ParameterType;

                    reader?.SetReceivedAction(message =>
                    {
                        var serviceInstance = app.ApplicationServices.GetService(messageQueueServiceType);

                        var deserializedMessage = JsonConvert.DeserializeObject(message, parameterType);

                        method.Invoke(serviceInstance, new[] {deserializedMessage});
                    });
                }
            }

            return app;
        }
    }
}