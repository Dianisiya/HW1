using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Wheather.Models.Db;
using Wheather.Services.Interfaces;

namespace Wheather.API
{
    public class CityController : Controller
    {
        private IRepository<City, int> db;
        public CityController (IRepository<City, int> db)
        {
            
            this.db = db;
        }


        public JsonResult GetCity()
        {
            return Json(db.Get(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCities(int id)
        {
            Models.Db.City cities = db.Get(id);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Http.HttpPost]
        public void CreateCity([FromBody] Models.Db.City city)
        {
            db.Add(city);
            db.Save();
        }

        [System.Web.Http.HttpPut]
        public void EditCity(int id, [FromBody]Models.Db.City city)
        {
            if(id == city.Id)
            {
                db.Update(city); 
                db.Save();
            }
        }

        public void DeleteCity(int id)
        {
            Models.Db.City city = db.Get(id);
            if(city!=null)
            {
                db.Delete(id);
                db.Save();
            }
        }
    }
}
