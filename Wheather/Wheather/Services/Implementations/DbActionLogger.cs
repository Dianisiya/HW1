using Wheather.Models.Db;
using Wheather.Services.Interfaces;

namespace Wheather.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
        public async Task AddAction(string action, IEnumerable<Weather> result = null)
        {
            var entity = new Action{DateTime = DateTime.Now, Description = action};
            await this._actionsRepository.Add(entity);
            if (result != null)
            {
                foreach (var weather in result)
                {
                    weather.ActionId = entity.Id;
                    await this._weatherRepository.Add(weather);
                }
            }
            await this._actionsRepository.Save();
        }
    }
}