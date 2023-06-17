using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Messages
{
    public class TestMessage : MessageBase
    {
        public object Message { get; set; } = default!;
    }
}
