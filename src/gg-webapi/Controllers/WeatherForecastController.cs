using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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
                TemperatureC = -17,
                ZipCode = "44011"
                
            },
            new WeatherForecast
            {
                Date = DateTime.Now.AddDays(-2),
                Summary = "Chilly",
                TemperatureC = 1,
                ZipCode = "44145"
            },
            new WeatherForecast
            {
                Date = DateTime.Now.AddDays(-1),
                Summary = "Cool",
                TemperatureC = 14,
                ZipCode = "44011"
            },
            new WeatherForecast
            {
                Date = DateTime.Now.AddDays(-4),
                Summary = "Humid",
                TemperatureC = 45,
                ZipCode = "44011"
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

        [HttpGet("{id}")]
        public WeatherForecast Get(int id)
        {
            return WeatherForecast.ElementAt(id - 1);
        }

        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecastInput input)
        {
            WeatherForecast.Add(new WeatherForecast { Date = DateTime.Now, TemperatureC = input.TemperatureC, Summary = input.Summary, ZipCode = input.ZipCode });
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

        [HttpPut(template: "{id}")]
        public IActionResult Put(int id, WeatherForecastInput input)
        {
            int indexToRemoveAt = id - 1;

            if (WeatherForecast.Count >= indexToRemoveAt)
            {
                var currentItem = WeatherForecast[indexToRemoveAt];
                WeatherForecast.RemoveAt(indexToRemoveAt);
                WeatherForecast.Add(new WeatherForecast { Date = currentItem.Date, Summary = input.Summary, TemperatureC = input.TemperatureC, ZipCode = input.ZipCode });
                return Ok("Replaced successfully");
            }

            return BadRequest("invalid index");
        }
    }
}
