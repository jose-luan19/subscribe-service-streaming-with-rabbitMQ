using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
namespace desafioBack.RabitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            using
            var channel = connection.CreateModel();

            string queue = "subs";

            channel.QueueDeclare(queue, exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: queue, body: body);
        }
    }
}