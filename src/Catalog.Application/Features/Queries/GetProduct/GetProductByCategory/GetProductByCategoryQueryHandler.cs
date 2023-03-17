using Catalog.Application.Interfaces.Persistence;
using Catalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Queries.GetProduct.GetProductByCategory
{
    public class GetProductByCategoryQueryHandler : IRequestHandler<GetProductByCategoryQuery, IEnumerable<Product>>
    {
        private readonly IRepository<Product> _productRepository;
        public GetProductByCategoryQueryHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAsync(x => x.Category.ToLower() == request.Category.ToLower());
        }
    }
}
