using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Wheather.Models;
using Wheather.Models.Now;
using Wheather.Models.Seven;
using Wheather.Models.Three;
using Wheather.Services.Interfaces;

namespace Wheather.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService;

        public HomeController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetWeatherNow(string city)
        {
            return PartialView("Present", _weatherService.GetPresentWeather(city));
        }

        public ActionResult GetWeatherThreeDays(string city)
        {
            return PartialView("ThreeDays", _weatherService.GetWeatherForThreeDays(city));
        }

        public ActionResult GetWeatherSevenDays(string city)
        {
            return PartialView("SevenDays", _weatherService.GetWeatherSevenDays(city));
        }
    }
}