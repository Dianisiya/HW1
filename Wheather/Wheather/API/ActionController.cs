using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Wheather.Models.Db;
using Wheather.Services.Interfaces;

namespace Wheather.API
{
    public class ActionController : Controller
    {
        private IRepository<Models.Db.Action, int> db;
        public ActionController(IRepository<Models.Db.Action, int> db)
        {
            this.db = db;
        }

        public JsonResult GetHistory()
        {
            var enumerable = this.db.Include("Result").Get();
            foreach(var weather in enumerable.SelectMany(a => a.Result))
            {
                weather.Action = null;
            }
            return this.Json(enumerable, JsonRequestBehavior.AllowGet);
        }
    }
}
