using System.Threading.Tasks;
using alanAPI.Models;
using System.Collections.Generic;

namespace alanAPI.Services
{
    public interface IOpenWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetChinaWeatherAsync();
    }
}