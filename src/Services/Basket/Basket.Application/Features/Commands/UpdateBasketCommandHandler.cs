using AutoMapper;
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
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, ShoppingCart>
    {
        private readonly IRepository<ShoppingCart> _repository;
        private readonly IMapper _mapper;
        public UpdateBasketCommandHandler(IRepository<ShoppingCart> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ShoppingCart> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = _mapper.Map<ShoppingCart>(request);
            return await _repository.AddAsync(shoppingCart);
        }
    }
}
