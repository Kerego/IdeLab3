
using Xamarin.Forms;

namespace CrossWeather
{
    public class App : Application
    {
        public App()
        {
			MainPage = new TabbedPage () {
				Children = {
					new WeatherPage (),
					new ForecastPage(),
					new SettingsPage()
				}
			};
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
