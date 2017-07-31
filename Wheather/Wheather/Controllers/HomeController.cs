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
    using System.Globalization;
    using System.Threading;
    using System.Web.Http.Cors;
    using System.Web.WebSockets;

    using Wheather.Models.Db;
    using Wheather.Services.Implementations;

    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }
    }

    [AllowCrossSiteJson]
    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService;

        private readonly IActionLogger actionLogger;

        private readonly IRepository<Action, int> _actionRepository;

        private readonly IRepository<City, int> _cityRepository;

        public HomeController(IWeatherService weatherService, IActionLogger actionLogger, IRepository<Action, int> actionRepository, IRepository<City, int> cityRepository)
        {
            _weatherService = weatherService;
            this.actionLogger = actionLogger;
            this._actionRepository = actionRepository;
            this._cityRepository = cityRepository;
        }

        
        public ActionResult Index()
        {
            return View(new IndexModel
                            {
                                Cities = this._cityRepository.Get().Select(c => c.Name).ToArray()
                            });
        }

        public ActionResult History()
        {
            var enumerable = this._actionRepository.Include("Result").Get();
            return View(new HistoryModel{History = enumerable.Reverse()});
        }

        public ActionResult GetHistory()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var enumerable = this._actionRepository.Include("Result").Get();
            enumerable.ToList().ForEach(a => a.Result.ToList().ForEach(w => w.Action = null));
            return Json(enumerable.Select(
                h => { return new { h.Description, h.Result, DateTime = h.DateTime.ToString("D") }; }),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCities()
        {
            return this.Json(this._cityRepository.Get().Select(c => c.Name), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddCity(string city)
        {
            if (this._cityRepository.Get().All(c => c.Name != city))
            {
                this._cityRepository.Add(new City { Name = city });
                this.actionLogger.AddAction($"User add new city : {city}");
                this._cityRepository.Save();
            }
        }

        [HttpPost]
        public void DeleteCity(string city)
        {
            var _city = this._cityRepository.Get().FirstOrDefault(c => c.Name == city);
            if (_city != null)
            {
                this._cityRepository.Delete(_city.Id);
                this.actionLogger.AddAction($"User delete new city : {city}");
                this._cityRepository.Save();
            }
        }

        [HttpPost]
        public void UpdateCities(string[] cities)
        {
            foreach (var city in this._cityRepository.Get().ToArray())
            {
                this._cityRepository.Delete(city.Id);
            }
            foreach (var city in cities)
            {
                this._cityRepository.Add(new City { Name = city });
            }
            this._cityRepository.Save();
            this.actionLogger.AddAction($"User update cities. New cities : {cities.Aggregate((s, s1) => s + "," + s1)}");
        }
        
        public ActionResult GetWeatherNow(string city)
        {
            var presentWeather = this._weatherService.GetPresentWeather(city);
            this.actionLogger.AddAction($"User view present weather in {city}", new []{new Weather { City = presentWeather.Name, DateTime = new DateTime(1970,1,1,0,0,0).AddSeconds(presentWeather.Dt), IconNumber = presentWeather.Weather[0].Icon} });
            return PartialView("Present", presentWeather);
        }

        public ActionResult GetWeatherThreeDays(string city)
        {
            var weatherForThreeDays = this._weatherService.GetWeatherForThreeDays(city);
            this.actionLogger.AddAction($"User view three days weather in {city}", weatherForThreeDays.List.Select(w => new Weather{City = weatherForThreeDays.City.Name, DateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(w.Dt), IconNumber = w.Weather[0].Icon}));
            return PartialView("ThreeDays", weatherForThreeDays);
        }

        public ActionResult GetWeatherSevenDays(string city)
        {
            var weatherSevenDays = this._weatherService.GetWeatherSevenDays(city);
            this.actionLogger.AddAction($"User view three days weather in {city}", weatherSevenDays.List.Select(w => new Weather { City = weatherSevenDays.City.Name, DateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(w.Dt), IconNumber = w.Weather[0].Icon }));
            return PartialView("SevenDays", weatherSevenDays);
        }

        public ActionResult GetWeatherNowJson(string city)
        {
            var presentWeather = this._weatherService.GetPresentWeather(city);
            this.actionLogger.AddAction($"User view present weather in {city}", new[] { new Weather { City = presentWeather.Name, DateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(presentWeather.Dt), IconNumber = presentWeather.Weather[0].Icon } });
            return Json(presentWeather, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWeatherThreeDaysJson(string city)
        {
            var weatherForThreeDays = this._weatherService.GetWeatherForThreeDays(city);
            this.actionLogger.AddAction($"User view three days weather in {city}", weatherForThreeDays.List.Select(w => new Weather { City = weatherForThreeDays.City.Name, DateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(w.Dt), IconNumber = w.Weather[0].Icon }));
            return Json(weatherForThreeDays, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWeatherSevenDaysJson(string city)
        {
            var weatherSevenDays = this._weatherService.GetWeatherSevenDays(city);
            this.actionLogger.AddAction($"User view three days weather in {city}", weatherSevenDays.List.Select(w => new Weather { City = weatherSevenDays.City.Name, DateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(w.Dt), IconNumber = w.Weather[0].Icon }));
            return Json(weatherSevenDays, JsonRequestBehavior.AllowGet);
        }
    }
}