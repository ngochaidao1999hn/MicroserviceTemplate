using RabbitMQ.Messages;

namespace Order.Api.Services
{
    public interface IBusService
    {
        Task SendAsync(TestMessage mess);
    }
}
