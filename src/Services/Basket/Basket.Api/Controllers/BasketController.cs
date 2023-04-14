using Basket.Application.Features.Commands;
using Basket.Application.Features.Queries;
using Basket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(int userId)
        {
            var shoppingCart = await _mediator.Send(new GetBasketByUserIdQuery(userId));
            return Ok(shoppingCart);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteBasket(int userId)
        {
            var res = await _mediator.Send(new DeleteBasketCommand(userId));
            return res ? Ok(): BadRequest();
        }

        [HttpPost("add-basket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddBasket([FromBody] UpdateBasketCommand command)
        {
           return Ok(await _mediator.Send(command));
        }
    }
}
