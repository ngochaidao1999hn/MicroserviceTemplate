using Catalog.Application.Interfaces.Persistence;
using Catalog.Infrastructure.Bus;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Catalog.Infrastructure
{
    public static class InfrastructureServiceResolver
    {
        public static IServiceCollection InfrastructureServiceRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CatalogConnectionString")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IConnection>(sp =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = configuration["RabbitMQ:Hostname"],
                    UserName = configuration["RabbitMQ:Username"],
                    Password = configuration["RabbitMQ:Password"]
                };
                return factory.CreateConnection();
            });
            services.AddSingleton<IModel>(sp =>
            {
                var connection = sp.GetRequiredService<IConnection>();
                var channel = connection.CreateModel();

                // Declare the queue
                channel.QueueDeclare(
                    queue: "orderFanDirect",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                return channel;
            });
            return services;
        }

        public static void Configue(IApplicationBuilder app)
        {
            app.ConfigureRabbitMQ();
        }

        public static void ConfigureRabbitMQ(this IApplicationBuilder app)
        {
            var channel = app.ApplicationServices.GetService<IModel>();
            BusConsumerRegister(app, channel);
        }

        private static void BusConsumerRegister(IApplicationBuilder app, IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, ea) =>
            {
                TestConsumer consumer = new TestConsumer();
                consumer.Consume(sender, ea);
                channel.BasicAck(ea.DeliveryTag, false);
            };
            channel.BasicConsume(queue: "orderFanDirect", autoAck: false, consumer: consumer);
        }
    }
}
