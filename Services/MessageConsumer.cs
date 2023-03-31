using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        private readonly IServiceProvider _serviceProvider;
        private ISubService _subService;

        public MessageConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

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

            consumer.Received += (model, eventArgs) =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    _subService = scope.ServiceProvider.GetRequiredService<ISubService>();

                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Product message received: " + message);

                    if (message.IndexOf("FullName") >= 0)
                    {
                        var user = JsonConvert.DeserializeObject<User>(message);
                        var saved = _subService.CreateSub(user);
                    }
                    else
                    {
                        var messageSplit = message.Split('[');
                        var check = messageSplit[1].Remove(messageSplit[1].IndexOf(']'));
                        var id = messageSplit[0] + "\"";

                        if (check == "CANCELED")
                        {
                            var sub = JsonConvert.DeserializeObject<Guid>(id);
                            _subService.CanceledSub(sub);
                        }
                        if (check == "RESTARTED")
                        {
                            var sub = JsonConvert.DeserializeObject<Guid>(id);
                            _subService.RestartedSub(sub);
                        }
                    }
                }
            };

            _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
