using System;
using TraxionWeather.ViewModels;
using Xunit;

namespace TraxionWeather.UnitTest
{
    [CollectionDefinition(nameof(SystemTestCollectionDefinition), DisableParallelization = true)]
    public class SystemTestCollectionDefinition { }

    [Collection(nameof(SystemTestCollectionDefinition))]
    public class TraxionUnitTest
    {
        [Fact]
        public void TestNormal()
        {
            WeatherViewModel weather = new WeatherViewModel();
            DateTime dateVal = DateTime.UtcNow;
            int dt = (Int32)(new TimeSpan(dateVal.Ticks) - new TimeSpan(new DateTime(1970, 1, 1).Ticks)).TotalSeconds;
            weather.GetWeatherDetails(dt.ToString());
            Assert.True(weather.overallWeatherInfo.current != null);
        }

        [Fact]
        public void TestPast5Days()
        {
            WeatherViewModel weather = new WeatherViewModel();
            DateTime dateVal = DateTime.UtcNow.AddDays(-6);
            int dt = (Int32)(new TimeSpan(dateVal.Ticks) - new TimeSpan(new DateTime(1970, 1, 1).Ticks)).TotalSeconds;
            weather.GetWeatherDetails(dt.ToString());
            Assert.True(weather.overallWeatherInfo.current == null);
        }

        [Fact]
        public void TestInvalid()
        {
            WeatherViewModel weather = new WeatherViewModel();
            weather.GetWeatherDetails("aaa111aa11");
            Assert.True(weather.overallWeatherInfo.current == null);
        }
    }
}