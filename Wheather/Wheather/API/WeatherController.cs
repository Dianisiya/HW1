using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wheather.Models.Seven;
using Wheather.Services.Interfaces;
using Wheather.Models;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace Wheather.API
{
    public class WeatherController : Controller
    {
        private IWeatherService ws;
        private IActionLogger al;
        public WeatherController (IWeatherService ws, IActionLogger al)
        {
            this.ws = ws;
            this.al = al;
        }

        public JsonResult GetWeatherNew (string city, int days)
        {
            var weatherSevenDays = this.ws.GetWeather(city, days);
            this.al.AddAction($"API GetRequest for {city} on {days} day(s)", weatherSevenDays.List.Select(w => new Wheather.Models.Db.Weather {City = weatherSevenDays.City.Name, DateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(w.Dt), IconNumber = w.Weather[0].Icon }));
            return Json(weatherSevenDays, JsonRequestBehavior.AllowGet);
        }
    }
}
