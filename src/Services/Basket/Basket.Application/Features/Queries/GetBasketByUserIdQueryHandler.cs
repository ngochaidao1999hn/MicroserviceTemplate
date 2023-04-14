using Basket.Application.Interfaces.Persistence;
using Basket.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Features.Queries
{
    public class GetBasketByUserIdQueryHandler : IRequestHandler<GetBasketByUserIdQuery, ShoppingCart>
    {
        private readonly IRepository<ShoppingCart> _repository;
        public GetBasketByUserIdQueryHandler(IRepository<ShoppingCart> repository)
        {
            _repository = repository;
        }
        public async Task<ShoppingCart> Handle(GetBasketByUserIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _repository.GetAsync(x => x.UserId == request.UserId);
            return res?.FirstOrDefault();
        }
    }
}
