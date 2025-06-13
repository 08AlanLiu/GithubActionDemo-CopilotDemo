using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using alanAPI.Services;

namespace alanAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IOpenWeatherService _openWeatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOpenWeatherService openWeatherService)
        {
            _logger = logger;
            _openWeatherService = openWeatherService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {   
            _logger.LogInformation("Fetching weather data for China cities");
            return await _openWeatherService.GetChinaWeatherAsync();
        }
    }
}
