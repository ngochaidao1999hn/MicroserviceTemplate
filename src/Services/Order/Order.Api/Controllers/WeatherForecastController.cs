using DiscountGrpc;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Order.Services;
using RabbitMQ.Bus;
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
        private readonly IEventBus _eventBus;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDiscountService discountService, IEventBus eventBus)
        {
            _logger = logger;
            _discountService = discountService;
            _eventBus = eventBus;
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
        public async Task<IActionResult> Send([FromBody] TestMessage request)
        {
            try
            {
                _eventBus.Publish<TestMessage>(request);
                return Ok();
            }
            catch (RpcException ex)
            {
                return BadRequest(ex.Status);
            }
        }
    }
}