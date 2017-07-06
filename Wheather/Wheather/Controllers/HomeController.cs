using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Wheather.Models;

namespace Wheather.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetWeatherNow(string city)
        {
            using (var client = new HttpClient())
            {
                var responce = client.GetStringAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric").Result;
                var res=JsonConvert.DeserializeObject<Now>(responce);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetWeatherThreeDays(string city)
        {
            using (var client = new HttpClient())
            {
                var responce = client.GetStringAsync($"http://api.openweathermap.org/data/2.5/forecast?q={city}&cnt=3&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric").Result;
                var res = JsonConvert.DeserializeObject<Three>(responce);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetWeatherSevenDays(string city)
        {
            using (var client = new HttpClient())
            {
                var responce = client.GetStringAsync($"http://api.openweathermap.org/data/2.5/forecast?q={city}&cnt=7&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric").Result;
                var res = JsonConvert.DeserializeObject<Seven>(responce);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

    }
}