using Wheather.Models.Db;
using Wheather.Services.Interfaces;

namespace Wheather.Services.Implementations
{
    using System;
    using System.Collections.Generic;

    using Action = Wheather.Models.Db.Action;

    public class DbActionLogger: IActionLogger
    {
        private readonly IRepository<Action, int> _actionsRepository;

        private readonly IRepository<Weather, int> _weatherRepository;

        public DbActionLogger(IRepository<Action, int> actionsRepository, IRepository<Weather, int> weatherRepository)
        {
            this._actionsRepository = actionsRepository;
            this._weatherRepository = weatherRepository;
        }
        public void AddAction(string action, IEnumerable<Weather> result = null)
        {
            var entity = new Action{DateTime = DateTime.Now, Description = action};
            this._actionsRepository.Add(entity);
            if (result != null)
            {
                foreach (var weather in result)
                {
                    weather.ActionId = entity.Id;
                    this._weatherRepository.Add(weather);
                }
            }
            this._actionsRepository.Save();
        }
    }
}