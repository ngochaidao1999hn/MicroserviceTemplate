using DiscountGrpc;
using Grpc.Core;
using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Services;
using Order.Services;
using RabbitMQ.Messages;

namespace Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDiscountService _discountService;
        private readonly IBusService _busService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDiscountService discountService, IBusService busService)
        {
            _logger = logger;
            _discountService = discountService;
            _busService = busService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("discount")]
        public async Task<IActionResult> GetDiscount([FromQuery]GetDiscountRequest request) 
        {
            try 
            { 
            var data = await _discountService.GetDiscount(request);
            return Ok(data);
            }
            catch (RpcException ex) 
            { 
                return BadRequest(ex.Status);
            }
        }

        [HttpPost("rabbitMQ")]
        public async Task<IActionResult> Send([FromBody] TestEvent request)
        {
            try
            {
                await _busService.SendAsync(request);
                return Ok();
            }
            catch (RpcException ex)
            {
                return BadRequest(ex.Status);
            }
        }
    }
}