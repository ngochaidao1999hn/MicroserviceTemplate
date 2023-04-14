using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Features.Commands
{
    public class DeleteBasketCommand: IRequest<bool>
    {
        public int UserId { get; set; }
        public DeleteBasketCommand(int userId)
        {
            this.UserId = userId;
        }
    }
}
