using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Messages
{
    public class MessageBase
    {
        public string Id { get; set; } = default!;
        public DateTime SendDate { get; set; } = default!;
    }
}
