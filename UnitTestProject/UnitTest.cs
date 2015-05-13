using System;
using System.Threading.Tasks;
using CrossWeather;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public async Task WrongAddress()
        {
            try
            {
                await ApiHelper.ApiCall<WeatherData>("");
                Assert.Fail();
            }
			catch { }
        }

        [TestMethod]
        public async Task WrongLocation()
        {
            Tuple<double, double> location = new Tuple<double, double>(404,404);
            var result = await ApiHelper.getGeoCode(location);
            Assert.AreEqual(result, "City not found!");
        }

        [TestMethod]
        public async Task WrongPlaceForecast()
        {
            ForecastViewModel viewModel = new ForecastViewModel() { city = "hfadasdagsd" };
            await ApiHelper.getForecast(viewModel);
            Assert.AreEqual("Connection", viewModel.data[0].date);  // if error happens first elements in list will be 2 labels with Connection Error text
        }

        [TestMethod]
        public async Task WrongPlaceWeather()
        {
            WeatherViewModel viewModel = new WeatherViewModel() { city = "hfadasdagsd" };
            await ApiHelper.getWeather(viewModel);
            Assert.AreEqual("Connection Error", viewModel.description);
        }
    }
}
