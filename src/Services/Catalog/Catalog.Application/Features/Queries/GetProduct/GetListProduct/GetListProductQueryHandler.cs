using Catalog.Application.Interfaces.Persistence;
using Catalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Queries.GetProduct.GetListProduct
{
    public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, IEnumerable<Product>>
    {
        private readonly IRepository<Product> _repository;
        public GetListProductQueryHandler(IRepository<Product> productRepository)
        {
            _repository = productRepository;
        }
        public async Task<IEnumerable<Product>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
