using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gg_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly List<WeatherForecast> WeatherForecast = new List<WeatherForecast>
        {
            new WeatherForecast
            {
                Date = DateTime.Now.AddDays(-3),
                Summary = "Freezing",
                TemperatureC = -17
            },
            new WeatherForecast
            {
                Date = DateTime.Now.AddDays(-2),
                Summary = "Chilly",
                TemperatureC = 1
            },
            new WeatherForecast
            {
                Date = DateTime.Now.AddDays(-1),
                Summary = "Cool",
                TemperatureC = 14
            },
        };
        
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return WeatherForecast.OrderBy(m => m.Date).ToList();
        }
        [HttpPost]
        public IActionResult Post([FromBody]WeatherForecastInput input)
        {
            WeatherForecast.Add(new WeatherForecast { Date = DateTime.Now, TemperatureC = input.TemperatureC, Summary = input.Summary });
            return Ok("Added successfully");
        }

        [HttpDelete]
        public IActionResult DeleteFirst()
        {
            if (WeatherForecast.Count > 0)
            {
                WeatherForecast.OrderBy(m => m.Date).ToList().RemoveAt(0);
                return Ok("First item deleted successfully");
            }
            return BadRequest();
        }
    }
}
