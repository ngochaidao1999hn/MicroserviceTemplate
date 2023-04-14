using Basket.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Features.Queries
{
    public class GetBasketByUserIdQuery: IRequest<ShoppingCart>
    {
        public int UserId { get; set; }
        public GetBasketByUserIdQuery(int userId)
        {
            this.UserId = userId;
        }
    }
}
