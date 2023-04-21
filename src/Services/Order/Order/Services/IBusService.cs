namespace Order.Api.Services
{
    public interface IBusService
    {
        Task SendAsync(object mess);
    }
}
