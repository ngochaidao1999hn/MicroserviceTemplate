using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumers;
using RabbitMQ.Messages;
using System.Text;

namespace Catalog.Infrastructure.Bus
{
    public class TestConsumer: ConsumerBase
    {
        public override void Consume(object sender, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var data = JsonConvert.DeserializeObject<TestEvent>(message);
            Console.WriteLine($"Received message: {data.message}");
            // Process the message here
        }
    }
}
