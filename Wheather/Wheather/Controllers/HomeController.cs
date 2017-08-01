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
    using System.Threading.Tasks;
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

        
        public async Task<ActionResult> Index()
        {
            return View(new IndexModel
                            {
                                Cities = (await this._cityRepository.Get()).Select(c => c.Name).ToArray()
                            });
        }

        public async Task<ActionResult> History()
        {
            var enumerable = await this._actionRepository.Include("Result").Get();
            return View(new HistoryModel{History = enumerable.Reverse()});
        }

        public async Task<ActionResult> GetHistory()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var enumerable = await this._actionRepository.Include("Result").Get();
            enumerable.ToList().ForEach(a => a.Result.ToList().ForEach(w => w.Action = null));
            return Json(enumerable.Select(
                h => { return new { h.Description, h.Result, DateTime = h.DateTime.ToString("D") }; }),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetCities()
        {
            return this.Json((await this._cityRepository.Get()).Select(c => c.Name), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task AddCity(string city)
        {
            if ((await this._cityRepository.Get()).All(c => c.Name != city))
            {
                await this._cityRepository.Add(new City { Name = city });
                await this.actionLogger.AddAction($"User add new city : {city}");
                await this._cityRepository.Save();
            }
        }

        [HttpPost]
        public async Task DeleteCity(string city)
        {
            var _city = (await this._cityRepository.Get()).FirstOrDefault(c => c.Name == city);
            if (_city != null)
            {
                await this._cityRepository.Delete(_city.Id);
                await this.actionLogger.AddAction($"User delete new city : {city}");
                await this._cityRepository.Save();
            }
        }

        [HttpPost]
        public async Task UpdateCities(string[] cities)
        {
            foreach (var city in (await this._cityRepository.Get()).ToArray())
            {
                await this._cityRepository.Delete(city.Id);
            }
            foreach (var city in cities)
            {
                await this._cityRepository.Add(new City { Name = city });
            }
            await this._cityRepository.Save();
            await this.actionLogger.AddAction($"User update cities. New cities : {cities.Aggregate((s, s1) => s + "," + s1)}");
        }
        
        public async Task<ActionResult> GetWeatherNow(string city)
        {
            var presentWeather = await this._weatherService.GetPresentWeather(city);
            await this.actionLogger.AddAction($"User view present weather in {city}", new []{new Weather { City = presentWeather.Name, DateTime = new DateTime(1970,1,1,0,0,0).AddSeconds(presentWeather.Dt), IconNumber = presentWeather.Weather[0].Icon} });
            return PartialView("Present", presentWeather);
        }

        public async Task<ActionResult> GetWeatherThreeDays(string city)
        {
            var weatherForThreeDays = await this._weatherService.GetWeatherForThreeDays(city);
            await this.actionLogger.AddAction($"User view three days weather in {city}", weatherForThreeDays.List.Select(w => new Weather{City = weatherForThreeDays.City.Name, DateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(w.Dt), IconNumber = w.Weather[0].Icon}));
            return PartialView("ThreeDays", weatherForThreeDays);
        }

        public async Task<ActionResult> GetWeatherSevenDays(string city)
        {
            var weatherSevenDays = await this._weatherService.GetWeatherSevenDays(city);
            await this.actionLogger.AddAction($"User view three days weather in {city}", weatherSevenDays.List.Select(w => new Weather { City = weatherSevenDays.City.Name, DateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(w.Dt), IconNumber = w.Weather[0].Icon }));
            return PartialView("SevenDays", weatherSevenDays);
        }

        public async Task<ActionResult> GetWeatherNowJson(string city)
        {
            var presentWeather = await this._weatherService.GetPresentWeather(city);
            await this.actionLogger.AddAction($"User view present weather in {city}", new[] { new Weather { City = presentWeather.Name, DateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(presentWeather.Dt), IconNumber = presentWeather.Weather[0].Icon } });
            return Json(presentWeather, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetWeatherThreeDaysJson(string city)
        {
            var weatherForThreeDays = await this._weatherService.GetWeatherForThreeDays(city);
            await this.actionLogger.AddAction($"User view three days weather in {city}", weatherForThreeDays.List.Select(w => new Weather { City = weatherForThreeDays.City.Name, DateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(w.Dt), IconNumber = w.Weather[0].Icon }));
            return Json(weatherForThreeDays, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetWeatherSevenDaysJson(string city)
        {
            var weatherSevenDays = await this._weatherService.GetWeatherSevenDays(city);
            await this.actionLogger.AddAction($"User view three days weather in {city}", weatherSevenDays.List.Select(w => new Weather { City = weatherSevenDays.City.Name, DateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(w.Dt), IconNumber = w.Weather[0].Icon }));
            return Json(weatherSevenDays, JsonRequestBehavior.AllowGet);
        }
    }
}