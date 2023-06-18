using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumers;
using RabbitMQ.Messages;
using RabbitMQ.Shared;
using System.Text;

namespace Catalog.Infrastructure.Bus
{
    public class TestConsumer : IDisposable
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly IConfiguration _config;
        public TestConsumer(IConfiguration config)
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
        public Task Consume()
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received message: {0}", message);
            };

            channel.BasicConsume(queue: RabbitMQConstants.TestQueueName, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            channel.Dispose();
            connection.Dispose();
        }
    }
}
