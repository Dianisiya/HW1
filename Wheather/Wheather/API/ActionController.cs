using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wheather.Models.Db;
using Wheather.Services.Interfaces;

namespace Wheather.API
{
    public class ActionController : ApiController
    {
        private IRepository<Models.Db.Action, int> db;
        public ActionController(IRepository<Models.Db.Action, int> db)
        {
            this.db = db;
        }

        public IEnumerable<Models.Db.Action> GetHistory()
        {
            return db.Include("Result").Get();
        }
    }
}
