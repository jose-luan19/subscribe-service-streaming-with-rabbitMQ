using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace desafioBack.Services
{
    public class MessageConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string queue = "subs";
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ISubService _subService;

        public MessageConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            using var scope = _serviceScopeFactory.CreateScope();
            _subService = scope.ServiceProvider.GetRequiredService<ISubService>();

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue, exclusive: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Product message received: " + message);

                if (message.IndexOf("FullName") >= 0)
                {
                   var user = JsonConvert.DeserializeObject<User>(message);
                   var saved = _subService.AddSub(user);
                }
                else
                {
                   var sub = JsonConvert.DeserializeObject<Guid>(message);
                }
            };

            _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
