using Catalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Queries.GetProduct.GetProductByCategory
{
    public class GetProductByCategoryQuery:IRequest<IEnumerable<Product>>
    {
        public string Category { get; set; }
        public GetProductByCategoryQuery(string Category)
        {
            this.Category = Category;
        }
    }
}
