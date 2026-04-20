using Newtonsoft.Json;
using System.Collections.Generic;

namespace alanAPI.Models
{
    public class OpenWeatherResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("main")]
        public MainWeather Main { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("dt")]
        public long DateTime { get; set; }
    }

    public class MainWeather
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("temp_min")]
        public double TempMin { get; set; }

        [JsonProperty("temp_max")]
        public double TempMax { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    public class Weather
    {
        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}