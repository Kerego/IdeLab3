using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Linq;
using CrossWeather.Forecast;

namespace CrossWeather
{
	public static class ApiHelper
	{
		static readonly HttpClient httpClient = new HttpClient();

		public static async Task<T> ApiCall<T>(string urlAddress)
		{
			HttpResponseMessage response = await httpClient.GetAsync(urlAddress);
			if (response.IsSuccessStatusCode) {
				string result = await response.Content.ReadAsStringAsync();
				T deserializationResult = await Task.Factory.StartNew (() => JsonConvert.DeserializeObject<T> (result));
				return deserializationResult;
			} else
				throw new Exception();
		}

		public static async Task<string> getLocation()
        {
            IGeolocation geolocation = DependencyService.Get<IGeolocation>();
            var location = await geolocation.getCurrentPosition();
            return await getGeoCode(location);
        }

        public static async Task<string> getGeoCode(Tuple<double, double> location)
        {
            try
            {
                LocationData locationData = await ApiCall<LocationData>("http://maps.googleapis.com/maps/api/geocode/json?latlng=" + location.Item1 + "," + location.Item2 + "&sensor=true");
                return locationData.results[0].address_components.Where(x => x.types[0] == "locality").Select(x => x.short_name).First();
            }
            catch
            {
                return "City not found!";
            }
        }

        public static async Task getWeather(WeatherViewModel weather)
		{
			try
			{
				WeatherData weatherData = await ApiCall<WeatherData>("http://api.openweathermap.org/data/2.5/weather?q=" + weather.city + "&units=metric&APPID=b2c4d74edeeef006030208cb1b64d902");
				weather.temperature = weatherData.main.temp.ToString();
				weather.description = weatherData.weather[0].description;
				weather.icon = weatherData.weather[0].icon;
				weather.windSpeed = weatherData.wind.speed.ToString();
				weather.windDirection = weatherData.wind.deg;
				weather.country = weatherData.sys.country;
			}
			catch
			{
				weather.description = "Connection Error";
				weather.temperature = "0";
				weather.icon = string.Empty;
				weather.windSpeed = "0";
				weather.windDirection = 0;
				weather.country = string.Empty;
			}
		}

		public static async Task getForecast(ForecastViewModel forecast)
        {
            try
            {
				ForecastData forecastData = await ApiCall<ForecastData>("http://api.openweathermap.org/data/2.5/forecast/daily?q=" + forecast.city + "&units=metric&cnt=10");
                await fillCollection(forecast, forecastData);
            }
            catch
            {
                forecast.data.Clear();
                forecast.data.Add(new ForecastViewModel.ForecastDay()
                {
                    date = "Connection",
                    temp = "Error"
                });
                return;
            }

        }

        private static async Task fillCollection(ForecastViewModel forecast, ForecastData forecastData)
        {
            int iterator = 0;
            foreach (var dayData in forecastData.list)
            {
                var timeNow = DateTime.Now.AddDays(iterator);
                forecast.data.Add(new ForecastViewModel.ForecastDay()
                {
                    temp = string.Format("{0} / {1}°C", dayData.temp.night, dayData.temp.day),
                    icon = string.Format("Icon{0}.png", dayData.weather[0].icon),
                    date = timeNow.DayOfWeek + ", " + timeNow.Day
                });
                await Task.Delay(80);
                iterator++;
            }
            
        }
    }
}

