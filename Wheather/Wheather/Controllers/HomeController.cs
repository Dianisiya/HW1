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
    using Wheather.Models.Db;

    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService;

        private readonly WeatherDb db;

        private readonly IActionLogger actionLogger;

        public HomeController(IWeatherService weatherService, WeatherDb db, IActionLogger actionLogger)
        {
            _weatherService = weatherService;
            this.db = db;
            this.actionLogger = actionLogger;
        }

        
        public ActionResult Index()
        {
            return View(new IndexModel
                            {
                                Cities = this.db.Cities.Select(c => c.Name).ToArray()
                            });
        }

        public ActionResult History()
        {
            return View(new HistoryModel{History = this.db.Actions.ToArray().Reverse()});
        }

        [HttpPost]
        public void AddCity(string city)
        {
            if (this.db.Cities.All(c => c.Name != city))
            {
                this.db.Cities.Add(new City { Name = city });
                this.db.SaveChanges();
                this.actionLogger.AddAction($"User add new city : {city}");
            }
        }

        [HttpPost]
        public void DeleteCity(string city)
        {
            var _city = this.db.Cities.FirstOrDefault(c => c.Name == city);
            if (_city != null)
            {
                this.db.Cities.Remove(_city);
                this.db.SaveChanges();
                this.actionLogger.AddAction($"User delete new city : {city}");
            }
        }

        [HttpPost]
        public void UpdateCities(string[] cities)
        {
            this.db.Cities.RemoveRange(this.db.Cities);
            this.db.Cities.AddRange(cities.Select(c => new City { Name = c }));
            this.db.SaveChanges();
            this.actionLogger.AddAction($"User update cities. New cities : {cities.Aggregate((s, s1) => s + "," + s1)}");
        }

        [HttpGet]
        public ActionResult GetWeatherNow(string city)
        {
            this.actionLogger.AddAction($"User view present weather in {city}");
            return PartialView("Present", _weatherService.GetPresentWeather(city));
        }

        public ActionResult GetWeatherThreeDays(string city)
        {
            this.actionLogger.AddAction($"User view three days weather in {city}");
            return PartialView("ThreeDays", _weatherService.GetWeatherForThreeDays(city));
        }

        public ActionResult GetWeatherSevenDays(string city)
        {
            this.actionLogger.AddAction($"User view seven days weather in {city}");
            return PartialView("SevenDays", _weatherService.GetWeatherSevenDays(city));
        }
    }
}