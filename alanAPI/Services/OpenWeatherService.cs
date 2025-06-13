using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using alanAPI.Models;

namespace alanAPI.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenWeatherConfig _config;
        private readonly ILogger<OpenWeatherService> _logger;
        
        // Major Chinese cities
        private readonly string[] ChineseCities = 
        {
            "Beijing", "Shanghai", "Guangzhou", "Shenzhen", "Chengdu"
        };

        public OpenWeatherService(HttpClient httpClient, IOptions<OpenWeatherConfig> config, ILogger<OpenWeatherService> logger)
        {
            _httpClient = httpClient;
            _config = config.Value;
            _logger = logger;
        }

        public async Task<IEnumerable<WeatherForecast>> GetChinaWeatherAsync()
        {
            var weatherForecasts = new List<WeatherForecast>();

            try
            {
                foreach (var city in ChineseCities)
                {
                    var weather = await GetWeatherForCityAsync(city);
                    if (weather != null)
                    {
                        weatherForecasts.Add(weather);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching weather data from OpenWeather API");
                // Return fallback data if API fails
                return GetFallbackWeatherData();
            }

            return weatherForecasts.Any() ? weatherForecasts : GetFallbackWeatherData();
        }

        private async Task<WeatherForecast> GetWeatherForCityAsync(string city)
        {
            try
            {
                var url = $"{_config.BaseUrl}/weather?q={city}&appid={_config.ApiKey}&units=metric";
                var response = await _httpClient.GetStringAsync(url);
                var weatherData = JsonConvert.DeserializeObject<OpenWeatherResponse>(response);

                return new WeatherForecast
                {
                    Date = DateTimeOffset.FromUnixTimeSeconds(weatherData.DateTime).DateTime,
                    TemperatureC = (int)Math.Round(weatherData.Main.Temperature),
                    Summary = $"{weatherData.Name}: {weatherData.Weather.FirstOrDefault()?.Description ?? "Clear"}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Failed to get weather for {city}");
                return null;
            }
        }

        private IEnumerable<WeatherForecast> GetFallbackWeatherData()
        {
            var rng = new Random();
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

            return ChineseCities.Select((city, index) => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = $"{city}: {summaries[rng.Next(summaries.Length)]}"
            }).ToArray();
        }
    }
}