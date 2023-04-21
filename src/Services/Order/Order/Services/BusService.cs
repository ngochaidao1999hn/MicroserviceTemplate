using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Order.Api.Services
{
    public class BusService : IBusService
    {
        private readonly IConfiguration _config;
        private string userName { get; }
        private string password { get; }
        public BusService(IConfiguration config)
        {
            _config = config;
            userName = _config["RabbitMQ:UserName"];
            password = _config["RabbitMQ:Password"];
        }
        public Task SendAsync(object mess)
        {
            byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mess));
            var factory = new ConnectionFactory { HostName = "localhost", UserName = userName, Password = password };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "orderFanDirect", type: ExchangeType.Direct);
            channel.QueueDeclare(queue: "orderFanDirect",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
            channel.QueueBind(queue: "orderFanDirect", exchange: "orderFanDirect", routingKey: "my-key");
            channel.BasicPublish("orderFanDirect", "my-key", body: message);
            return Task.CompletedTask;
        }
    }
}
