using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossWeather
{
    public partial class WeatherPage : ContentPage
    {
        public WeatherPage()
        {
            InitializeComponent();
            Title = "Weather";

            stack.Padding = new Thickness(20, 20, 20, 20);
            Entry entry = new Entry() { Placeholder = "Enter the name of city", HorizontalOptions = LayoutOptions.Center, WidthRequest = 250 };
            stack.Children.Add(entry);

            Button button = new Button() { Text = "Get Weather", HorizontalOptions = LayoutOptions.Center, WidthRequest = 250 };
            stack.Children.Add(button);


            Label city = new Label() { FontSize = 35 };
            stack.Children.Add(city);

            Label overallWeather = new Label() { FontSize = 35 };
            stack.Children.Add(overallWeather);

            Label wind = new Label() { FontSize = 35 };
            stack.Children.Add(wind);

            Image image = new Image() { Aspect = Aspect.Fill, Source = ImageSource.FromFile("arrow.png"), HorizontalOptions = LayoutOptions.Center, WidthRequest = 100, HeightRequest = 100 };
            stack.Children.Add(image);



            button.Clicked += async (s, e) =>
            {
                button.Text = "Fetching...";
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync("http://api.openweathermap.org/data/2.5/weather?q=" + entry.Text + "&units=metric&APPID=b2c4d74edeeef006030208cb1b64d902");
                if (response.IsSuccessStatusCode == true)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(result);
                    if (result != null &&
                    result.Length >= 50)
                    {
                        city.Text = "Weather in " + weatherData.name;
                        overallWeather.Text = weatherData.weather[0].description + "\nTemperature: " + weatherData.main.temp + "°C";
                        wind.Text = "Wind Speed: " + weatherData.wind.speed;
                        button.Text = "Get Weather";
                        await image.RotateTo(weatherData.wind.deg, 800, Easing.CubicInOut);
                    }
                }
                else
                {
                    button.Text = "Get Weather";
                    city.Text = "Connection Error or city not found";
                }


            };

        }
    }
}
