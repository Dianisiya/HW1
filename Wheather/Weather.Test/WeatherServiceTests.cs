using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


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
    public class WeatherServiceTests
    {
        [Test]
        public void Correct_city_or_not_TestNow()
        {
            //Arrange
            var mock = new Mock<IRequestService>();
            mock.Setup(r => r.ExecuteGetRequest<Now>(It.IsAny<string>()))
                .Returns<string>(str => new Now { Name = str });
            var weather = new WeatherService(mock.Object);

            //Act
            var res = weather.GetPresentWeather("Lviv");

            //Assert
            Assert.AreEqual("http://api.openweathermap.org/data/2.5/weather?q=Lviv&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric", res.Name);
        }

        [Test]
        public void Correct_city_or_not_TestThree()
        {
            //Arrange
            var mock = new Mock<IRequestService>();
            mock.Setup(r => r.ExecuteGetRequest<Three>(It.IsAny<string>()))
                .Returns<string>(str => new Three { City = new Wheather.Models.Three.City { Name = str } });
            var weather = new WeatherService(mock.Object);

            //Act
            var res = weather.GetWeatherForThreeDays("Lviv");

            //Assert
            Assert.AreEqual("http://api.openweathermap.org/data/2.5/forecast/daily?q=Lviv&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric&cnt=3", res.City.Name);
        }

        [Test]
        public void Correct_city_or_not1_TestThree()
        {
            //Arrange
            var mock = new Mock<IRequestService>();
            mock.Setup(r => r.ExecuteGetRequest<Three>(It.IsAny<string>()))
                .Returns<string>(str => new Three { City = new Wheather.Models.Three.City { Name = str } });
            var weather = new WeatherService(mock.Object);

            //Act
            var res = weather.GetWeatherForThreeDays("Kyiv");

            //Assert
            Assert.AreEqual("http://api.openweathermap.org/data/2.5/forecast/daily?q=Kyiv&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric&cnt=3", res.City.Name);
        }

        [Test]
        public void Correct_city_or_not_TestSeven()
        {
            //Arrange
            var mock = new Mock<IRequestService>();
            mock.Setup(r => r.ExecuteGetRequest<Seven>(It.IsAny<string>()))
                .Returns<string>(str => new Seven { City = new Wheather.Models.Seven.City { Name = str } });
            var weather = new WeatherService(mock.Object);

            //Act
            var res = weather.GetWeatherSevenDays("Lviv");

            //Assert
            Assert.AreEqual("http://api.openweathermap.org/data/2.5/forecast/daily?q=Lviv&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric&cnt=7", res.City.Name);
        }

        [Test]
        public void Correct_city_or_not1_TestSeven()
        {
            //Arrange
            var mock = new Mock<IRequestService>();
            mock.Setup(r => r.ExecuteGetRequest<Seven>(It.IsAny<string>()))
                .Returns<string>(str => new Seven { City = new Wheather.Models.Seven.City { Name = str } });
            var weather = new WeatherService(mock.Object);

            //Act
            var res = weather.GetWeatherSevenDays("Odessa");

            //Assert
            Assert.AreEqual("http://api.openweathermap.org/data/2.5/forecast/daily?q=Odessa&APPID=dc190a9f47022fdf0ead666741607ed0&units=metric&cnt=7", res.City.Name);
        }
        /*   
          [Test]
          [Exception(typeof(ArgumentException))]
          public void TestNowException()
          {
              throw new ArgumentException();
          }
        */

    }
}
