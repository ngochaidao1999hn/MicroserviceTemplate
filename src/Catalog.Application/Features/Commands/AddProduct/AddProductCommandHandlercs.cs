using Catalog.Application.Interfaces.Persistence;
using Catalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Commands.AddProduct
{
    public class AddProductCommandHandlercs : IRequestHandler<AddProductCommand>
    {
        private readonly IRepository<Product> _productRepository;
        public AddProductCommandHandlercs(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            Product p = new Product();
            p.Name = request.Name;
            p.Description = request.Description;
            p.Category = request.Category;
            p.Summary = request.Summary;
            p.Price = request.Price;
            p.ImageFile = request.ImageFile;
            await _productRepository.AddAsync(p);
            return Unit.Value;
        }
    }
}
