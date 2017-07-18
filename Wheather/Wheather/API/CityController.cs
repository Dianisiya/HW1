using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wheather.Models.Db;
using Wheather.Services.Interfaces;

namespace Wheather.API
{
    public class CityController : ApiController
    {
        private IRepository<City, int> db;
        public CityController (IRepository<City, int> db)
        {
            
            this.db = db;
        }


        public IEnumerable<Models.Db.City> GetCity()
        {
            return db.Get();
        }

        public Models.Db.City GetCities(int id)
        {
            Models.Db.City cities = db.Get(id);
            return cities;
        }

        [HttpPost]
        public void CreateCity([FromBody] Models.Db.City city)
        {
            db.Add(city);
            db.Save();
        }

        [HttpPut]
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
