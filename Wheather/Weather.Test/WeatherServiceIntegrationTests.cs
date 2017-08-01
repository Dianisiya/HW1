using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Test
{
    using Moq;

    using NUnit.Framework;

    using Wheather.Services.Interfaces;
    using Wheather.Models.Now;
    using Wheather.Services.Implementations;
    using Wheather.Models.Three;
    using Wheather.Models.Seven;

    [TestFixture]
    class WeatherServiceIntegrationTests
    {
        [Test]
        public async Task The_same_or_not_city_and_time_Now()
        {
            //Arrange
            var weather = new WeatherService(new RequestService());

            //Act
            var res = await weather.GetPresentWeather("Lviv");

            //Assert
            Assert.AreEqual("Lviv", res.Name);

            
            Assert.AreEqual(DateTime.Now.Date, new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(res.Dt).Date);
            
        }

        [Test]
        public async Task The_same_or_not_city_and_time_Next_tree_days()
        {
            //Arrange
            var weather = new WeatherService(new RequestService());

            //Act
            var res = await weather.GetWeatherForThreeDays("Lviv");

            //Assert
            Assert.AreEqual("Lviv", res.City.Name);

            for (var i = 0; i < 3; i++)
            {
                Assert.AreEqual(DateTime.Now.AddDays(i).Date, new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(res.List[i].Dt).Date);
            }
        }

        [Test]
        public async Task The_same_or_not_city_and_time_Next_seven_days()
        {
            //Arrange
            var weather = new WeatherService(new RequestService());

            //Act
            var res = await weather.GetWeatherSevenDays("Lviv");

            //Assert
            Assert.AreEqual("Lviv", res.City.Name);

            for (var i = 0; i < 7; i++)
            {
                Assert.AreEqual(DateTime.Now.AddDays(i).Date, new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(res.List[i].Dt).Date);
            }
        }
    

        [Test]
        public async Task Count_of_day_Where_day_must_be_three()
        {
            //Arrange
            var weather = new WeatherService(new RequestService());

            //Act
            var res = await weather.GetWeatherForThreeDays("Lviv");

            //Assert
            Assert.AreEqual(3, res.List.Length);
        }

        [Test]
        public async Task Count_of_day_Where_day_must_be_seven()
        {
            //Arrange
            var weather = new WeatherService(new RequestService());

            //Act
            var res = await weather.GetWeatherSevenDays("Lviv");

            //Assert
            Assert.AreEqual(7, res.List.Length);
        }
    }
}
