using Wheather.Models.Db;
using Wheather.Services.Interfaces;

namespace Wheather.Services.Implementations
{
    using System;

    using Action = Wheather.Models.Db.Action;

    public class DbActionLogger: IActionLogger
    {
        private readonly WeatherDb db;

        public DbActionLogger(WeatherDb db)
        {
            this.db = db;
        }
        public void AddAction(string action)
        {
            this.db.Actions.Add(new Action{DateTime = DateTime.Now, Description = action});
            this.db.SaveChanges();
        }
    }
}