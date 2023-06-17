using MassTransit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumers;
using RabbitMQ.Messages;
using System.Text;

namespace Catalog.Infrastructure.Bus
{
    public class TestConsumer : IConsumer<TestMessage>
    {
        public Task Consume(ConsumeContext<TestMessage> context)
        {
            var data = context.Message.Message.ToString();
            Console.WriteLine(data);
            return Task.CompletedTask;
        }
    }
}
