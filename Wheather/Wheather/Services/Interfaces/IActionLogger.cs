using Wheather.Models.Db;

namespace Wheather.Services.Interfaces
{
    using System.Collections.Generic;

    public interface IActionLogger
    {
        void AddAction(string action, IEnumerable<Weather> result = null);
    }
}