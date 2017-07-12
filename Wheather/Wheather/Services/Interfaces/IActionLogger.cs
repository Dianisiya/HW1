using Wheather.Models.Db;

namespace Wheather.Services.Interfaces
{
    public interface IActionLogger
    {
        void AddAction(string action);
    }
}