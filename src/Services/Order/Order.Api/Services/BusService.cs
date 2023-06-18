using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Messages;
using RabbitMQ.Shared;
using System.Text;
using System.Threading.Channels;

namespace Order.Api.Services
{
    public class BusService : IBusService,IDisposable
    {
        private readonly IConfiguration _config;
        private readonly IConnection connection;
        private readonly IModel channel;
        public BusService(IConfiguration config)
        {
            _config = config;
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMQ:Hostname"],
                UserName = _config["RabbitMQ:Username"],
                Password = _config["RabbitMQ:Password"]
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }
        public Task SendAsync(TestMessage mess)
        {
            byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mess));
            channel.ExchangeDeclare(exchange: RabbitMQConstants.TestQueueName, type: ExchangeType.Fanout);
            channel.QueueDeclare(queue: RabbitMQConstants.TestQueueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
            channel.QueueBind(queue: RabbitMQConstants.TestQueueName, exchange: RabbitMQConstants.TestQueueName, routingKey: "");
            channel.BasicPublish(RabbitMQConstants.TestQueueName, "", body: message);            
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            channel.Dispose();
            connection.Dispose();
        }
    }
}
