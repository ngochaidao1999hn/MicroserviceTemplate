using RabbitMQ.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Bus
{
    public interface IEventHandler<in TEvent> where TEvent : Event
    {
        Task Handler(TEvent @event);
    }
}
