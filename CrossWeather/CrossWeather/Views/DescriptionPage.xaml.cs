using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossWeather
{
    public partial class DescriptionPage : ContentPage
    {
        Tuple<double,double> location;
        IGeolocation geolocation;
        Settings settings = new Settings();
        public DescriptionPage()
        {
            BindingContext = settings;
            InitializeComponent();
            settings.useGeoLocation = true;
            Title = "Description";

            stack.Padding = new Thickness(20, 20, 20, 20);
            geolocation = DependencyService.Get<IGeolocation>();

            //Button geoButton = new Button() { BackgroundColor = Color.Green, Text = "My Location", HorizontalOptions = LayoutOptions.Center, WidthRequest = 250 };
            //stack.Children.Add(geoButton);

            //Label city = new Label() { FontSize = 8 };
            //stack.Children.Add(city);

            //Label latitude = new Label() { FontSize = 25 };
            //stack.Children.Add(latitude);

            //Label longitude = new Label() { FontSize = 25 };
            //stack.Children.Add(longitude);

            //Label overallWeather = new Label() { FontSize = 35 };
            //stack.Children.Add(overallWeather);

            //Label wind = new Label() { FontSize = 35 };
            //stack.Children.Add(wind);

            //Image image = new Image() { Aspect = Aspect.Fill, Source = ImageSource.FromFile("arrow.png"), HorizontalOptions = LayoutOptions.Center, WidthRequest = 100, HeightRequest = 100 };
            //stack.Children.Add(image);

            //MessagingCenter.Subscribe<IGeolocation, Tuple<double,double>>(this, "LocationChanged", (sender, arg) =>
            //{
            //    location = arg;
            //});

            geoButton.Clicked += async (s, e) =>
                await getLocation ();

        }


        async Task getLocation()
        {
            geoButton.Text = "Fetching...";

            location = await geolocation.getCurrentPosition();

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("http://maps.googleapis.com/maps/api/geocode/json?latlng=" + location.Item1 + "," + location.Item2 + "&sensor=true");
            //HttpResponseMessage response = await httpClient.GetAsync("http://dev.virtualearth.net/REST/v1/Locations/" + location.Item1 + "," + location.Item2 + "?o=json&key=AsJ7Krlbmm6I_UqeJCAR4CsDP8x2Sod82oN1fYy1Z1TEredrt0o13X1A0PLk4t8g");
            if (response.IsSuccessStatusCode == true)
            {
                string result = await response.Content.ReadAsStringAsync();
                LocationData locationData = JsonConvert.DeserializeObject<LocationData>(result);
                string slocation = locationData.results[locationData.results.Length - (locationData.results.Length == 1 ? 1 : 2)].address_components[0].short_name;
                city.Text = "Your Location: " + slocation;
                latitude.Text = "latitude: " + location.Item1;
                longitude.Text = "longitude: " + location.Item2;
                await getWeather(slocation);
            }
            else
                city.Text = "Connection Error or city not found";
            geoButton.Text = "My location";
        }

        async Task getWeather(string place)
        {

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage     response = await httpClient.GetAsync("http://api.openweathermap.org/data/2.5/weather?q=" + place + "&units=metric&APPID=b2c4d74edeeef006030208cb1b64d902");
            if (response.IsSuccessStatusCode == true)
            {
                string result = await response.Content.ReadAsStringAsync();
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(result);
                if (result != null &&
                result.Length >= 50)
                {
                    overallWeather.Text = weatherData.weather[0].description + "\nTemperature: " + weatherData.main.temp + "°C";
                    wind.Text = "Wind Speed: " + weatherData.wind.speed;
                    await image.RotateTo(weatherData.wind.deg, 800, Easing.CubicInOut);
                }
            }
            else
                city.Text = "Connection Error";
        }
    }
}
