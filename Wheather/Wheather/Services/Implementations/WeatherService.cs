using System.Net.Http;
using Newtonsoft.Json;
using Wheather.Models.Now;
using Wheather.Models.Seven;
using Wheather.Models.Three;
using Wheather.Services.Interfaces;

namespace Wheather.Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        private readonly IRequestService _requestService;

        public WeatherService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Now GetPresentWeather(string city)
        {
            return _requestService.ExecuteGetRequest<Now>($"http://api.openweathermap.org/data/2.5/weather?q={city}&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric");
        }

        public Three GetWeatherForThreeDays(string city)
        {
            return _requestService.ExecuteGetRequest<Three>($"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric&cnt=3");
        }

        public Seven GetWeatherSevenDays(string city)
        {
            return _requestService.ExecuteGetRequest<Seven>($"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric&cnt=7");
        }
    }
}