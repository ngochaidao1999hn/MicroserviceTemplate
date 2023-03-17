using Catalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Queries.GetProduct.GetListProduct
{
    public class GetListProductQuery : IRequest<IEnumerable<Product>>
    {
    }
}
