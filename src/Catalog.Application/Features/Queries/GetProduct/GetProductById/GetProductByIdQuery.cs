using Catalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Queries.GetProduct.GetProductById
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
        public GetProductByIdQuery(int Id)
        {
            this.Id = Id;
        }
    }
}
