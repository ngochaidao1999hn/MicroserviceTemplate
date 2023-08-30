using RabbitMQ.Bus;
using RabbitMQ.Messages;

namespace Order.Api.Bus
{
    public class TestMessageHandler : IEventHandler<TestMessage>
    {
        public Task Handler(TestMessage @event)
        {
            Console.WriteLine(@event.Message);
            return Task.CompletedTask;
        }
    }
}
