using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossWeather
{
    public partial class DescriptionPage : ContentPage
    {
        Tuple<double, double> location;
        IGeolocation geolocation;
        Settings settings = new Settings();

		Image image = new Image() { Aspect = Aspect.Fill, Source = ImageSource.FromFile("arrow.png"), HorizontalOptions = LayoutOptions.Center, WidthRequest = 100, HeightRequest = 100 };
        
        public DescriptionPage()
        {
            BindingContext = settings;
            InitializeComponent();
            Title = "Description";

			stack.Children.Add(image);
            stack.Padding = new Thickness(20, 20, 20, 20);
            geolocation = DependencyService.Get<IGeolocation>();

            MessagingCenter.Subscribe<IGeolocation, Tuple<double, double>>(this, "LocationChanged", (sender, arg) =>
            {
                location = arg;
            });

            geoButton.Clicked += async (s, e) =>
                await getLocation();
        }


        async Task getLocation()
        {
            geoButton.Text = "Fetching...";
            settings.useGeoLocation = false;

            location = await geolocation.getCurrentPosition();
            LocationData locationData = await ApiCall<LocationData>("http://maps.googleapis.com/maps/api/geocode/json?latlng=" + location.Item1 + "," + location.Item2 + "&sensor=true");
            //HttpResponseMessage response = await httpClient.GetAsync("http://dev.virtualearth.net/REST/v1/Locations/" + location.Item1 + "," + location.Item2 + "?o=json&key=AsJ7Krlbmm6I_UqeJCAR4CsDP8x2Sod82oN1fYy1Z1TEredrt0o13X1A0PLk4t8g");
            
            string slocation = locationData.results[locationData.results.Length - (locationData.results.Length == 1 ? 1 : 2)].address_components[0].short_name;
            city.Text = "Your Location: " + slocation;
            latitude.Text = "latitude: " + location.Item1;
            longitude.Text = "longitude: " + location.Item2;
            await getWeather(slocation);
            geoButton.Text = "My location";
        }

        async Task<T> ApiCall<T>(string urlAddress)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(urlAddress);
            if (response.IsSuccessStatusCode == true)
            {
                string result = await response.Content.ReadAsStringAsync();
                T deserializationResult = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(result));
                return deserializationResult;
            }
            else
            {
                throw new Exception();
            }
        }

        async Task getWeather(string place)
        {
            try
            {
                WeatherData weatherData = await ApiCall<WeatherData>("http://api.openweathermap.org/data/2.5/weather?q=" + place + "&units=metric&APPID=b2c4d74edeeef006030208cb1b64d902");
                overallWeather.Text = weatherData.weather[0].description + "\nTemperature: " + weatherData.main.temp + "°C";
                wind.Text = "Wind Speed: " + weatherData.wind.speed;
                await image.RotateTo(weatherData.wind.deg, 800, Easing.CubicInOut);
            }
            catch
            {
                city.Text = "Connection Error";
            }
            settings.useGeoLocation = true;
        }
    }
}
