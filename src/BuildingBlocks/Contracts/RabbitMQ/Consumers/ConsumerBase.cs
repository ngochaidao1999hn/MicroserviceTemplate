using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumers
{
    public abstract class ConsumerBase
    {
        public abstract void Consume(object sender, BasicDeliverEventArgs ea);
    }
}
