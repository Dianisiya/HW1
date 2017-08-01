using System.Threading.Tasks;
using Wheather.Models.Now;
using Wheather.Models.Seven;
using Wheather.Models.Three;

namespace Wheather.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<Now> GetPresentWeather(string city);

        Task<Three> GetWeatherForThreeDays(string city);

        Task<Seven> GetWeatherSevenDays(string city);

        Task<Seven> GetWeather(string city, int days);
    }
}
