using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace gg_webapi.Controllers
{
    public class WeatherForecastInput
    {
        [Required]
        [JsonPropertyName("TemperatureC")]
        public int TemperatureC { get; }

        [Required]
        [JsonPropertyName("Summary")]
        public string Summary { get; }

        [JsonPropertyName("ZipCode")]
        public string ZipCode { get; }

        [JsonConstructor]
        public WeatherForecastInput(int temperatureC, string summary, string zipCode)
        {
            TemperatureC = temperatureC;
            Summary = summary;
            ZipCode = zipCode;
        }
    }
}
