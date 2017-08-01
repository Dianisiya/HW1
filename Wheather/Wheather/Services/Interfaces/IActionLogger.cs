using Wheather.Models.Db;

namespace Wheather.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IActionLogger
    {
        Task AddAction(string action, IEnumerable<Weather> result = null);
    }
}