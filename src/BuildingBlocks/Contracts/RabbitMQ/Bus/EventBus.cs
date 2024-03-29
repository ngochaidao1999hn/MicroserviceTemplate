﻿using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Events;
using System.Text;

namespace RabbitMQ.Bus
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<string, List<Type>> _handlers = new Dictionary<string, List<Type>>();
        private readonly List<Type> _eventTypes = new List<Type>();
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConnection connection;
        private readonly IModel channel;
        public EventBus(IServiceScopeFactory serviceScopeFactory)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _serviceScopeFactory = serviceScopeFactory;
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }
        public void Publish<T>(T @event) where T : Event
        {

            var eventName = @event.GetType().Name;

            channel.QueueDeclare(eventName, false, false, false, null);

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish("", eventName, null, body);

        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            if (_eventTypes is null || !_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (_handlers is null || !_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already is registered for '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName].Add(handlerType);
            StartBasicConsume<T>();
        }

        private Task StartBasicConsume<T>() where T : Event
        {
            var eventName = typeof(T).Name; 
            channel.QueueDeclare(eventName, false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                ProcessEvent(eventName, message);           
            };
            channel.BasicConsume(eventName, true, consumer);
            return Task.CompletedTask;

        }
          
        private Task ProcessEvent(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var subscriptions = _handlers[eventName];
                    foreach (var subscription in subscriptions)
                    {
                        var handler = scope.ServiceProvider.GetService(subscription);
                        if (handler is null) continue;
                        var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                        if (eventType is null) continue;
                        var @event = JsonConvert.DeserializeObject(message, eventType);
                        var conreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                        if (conreteType is null) continue;
                        conreteType.GetMethod("Handler")?.Invoke(handler, new object[] { @event });
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
