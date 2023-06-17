using Catalog.Application.Features.Commands.AddProduct;
using Catalog.Application.Features.Queries.GetProduct.GetListProduct;
using Catalog.Application.Features.Queries.GetProduct.GetProductByCategory;
using Catalog.Application.Features.Queries.GetProduct.GetProductById;
using Catalog.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _mediator.Send(new GetListProductQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var products = await _mediator.Send(new GetProductByIdQuery(id));
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpGet("GetProductByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory([FromQuery]string category)
        {
            var products = await _mediator.Send(new GetProductByCategoryQuery(category));
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] AddProductCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception e)
            { 
                return BadRequest(e);
            }
            
        }
    }
}
