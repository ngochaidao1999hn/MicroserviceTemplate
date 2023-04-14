using Basket.Application.Interfaces.Persistence;
using Basket.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Features.Commands
{
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand,bool>
    {
        private readonly IRepository<ShoppingCart> _repository;
        public DeleteBasketCommandHandler(IRepository<ShoppingCart> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var listShoppingCart = await _repository.GetAsync(x => x.UserId == request.UserId);
            var shoppingCart = listShoppingCart.FirstOrDefault();
            if (shoppingCart != null)
            {
                await _repository.UpdateAsync(shoppingCart);
                return true;
            }
            return false;

        }
    }
}
