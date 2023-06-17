using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumers
{
    public abstract class ConsumerBase
    {
        public abstract void Consume(MessageBase message, BasicDeliverEventArgs ea);
    }
}
