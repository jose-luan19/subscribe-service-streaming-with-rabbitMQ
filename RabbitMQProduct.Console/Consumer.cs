using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


//Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
var factory = new ConnectionFactory
{
    HostName = "localhost"
};

string queue = "subs";

var connection = factory.CreateConnection();

using
var channel = connection.CreateModel();
channel.QueueDeclare(queue, exclusive: false);
var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine("Product message received: " + message);

    if (message.IndexOf("FullName") >= 0)
    {
        var user = JsonConvert.DeserializeObject<User>(message);
    }
    else
    {
        var sub = JsonConvert.DeserializeObject<Guid>(message);
    }
};

channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
Console.ReadKey();