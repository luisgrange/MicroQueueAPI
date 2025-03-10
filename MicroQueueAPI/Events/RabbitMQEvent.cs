using RabbitMQ.Client;
using System.Text;

namespace MicroQueueAPI.Event
{
    public class RabbitMQEvent
    {
        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;

        public RabbitMQEvent(IConfiguration configuration)
        {
            _hostName = configuration["RabbitMQ:HostName"];
            _userName = configuration["RabbitMQ:UserName"];
            _password = configuration["RabbitMQ:Password"];
        }

        public void PublishMessage(string queueName, string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
