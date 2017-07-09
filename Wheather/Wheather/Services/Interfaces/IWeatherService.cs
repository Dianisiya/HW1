using Wheather.Models.Now;
using Wheather.Models.Seven;
using Wheather.Models.Three;

namespace Wheather.Services.Interfaces
{
    public interface IWeatherService
    {
        Now GetPresentWeather(string city);

        Three GetWeatherForThreeDays(string city);

        Seven GetWeatherSevenDays(string city);
    }
}
