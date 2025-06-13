# OpenWeather API Integration

This project has been updated to integrate with the OpenWeather API to show real weather data for major Chinese cities.

## Configuration

To use the OpenWeather API, you need to:

1. Get a free API key from [OpenWeatherMap](https://openweathermap.org/api)
2. Update the `appsettings.json` file with your API key:

```json
{
  "OpenWeather": {
    "ApiKey": "your_actual_api_key_here",
    "BaseUrl": "https://api.openweathermap.org/data/2.5",
    "DefaultCity": "Beijing"
  }
}
```

## Features

- Fetches real weather data for major Chinese cities: Beijing, Shanghai, Guangzhou, Shenzhen, and Chengdu
- Provides fallback data if the API is unavailable or the API key is invalid
- Uses metric units (Celsius) for temperature
- Includes city names in the weather summary

## API Endpoint

The weather endpoint remains the same: `/WeatherForecast`

The response format is compatible with the original API but now includes real weather data from China.