using System.Net.Http;
using Newtonsoft.Json;
using Wheather.Models.Now;
using Wheather.Models.Seven;
using Wheather.Models.Three;
using Wheather.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Wheather.Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        private readonly IRequestService _requestService;

        public WeatherService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<Now> GetPresentWeather(string city)
        {
            if((city == null) || (city == ""))
            {
                throw new ArgumentException();
            }
            return await _requestService.ExecuteGetRequest<Now>($"http://api.openweathermap.org/data/2.5/weather?q={city}&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric");
        }

        public async Task<Three> GetWeatherForThreeDays(string city)
        {
            if ((city == null) || (city == ""))
            {
                throw new ArgumentException();
            }
            return await _requestService.ExecuteGetRequest<Three>($"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric&cnt=3");
        }

        public async Task<Seven> GetWeatherSevenDays(string city)
        {
            if ((city == null) || (city == ""))
            {
                throw new ArgumentException();
            }
            return await _requestService.ExecuteGetRequest<Seven>($"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric&cnt=7");
        }
        public async Task<Seven> GetWeather(string city, int days)
        {
            if ((city == null) || (city == "") || (days<1))
            {
                throw new ArgumentException();
            }
            return await _requestService.ExecuteGetRequest<Seven>($"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric&cnt={days}");
        }
    }
}