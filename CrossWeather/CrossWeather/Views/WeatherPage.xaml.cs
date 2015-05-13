using Xamarin.Forms;
using System.Threading.Tasks;

namespace CrossWeather
{
    public partial class WeatherPage : ContentPage
	{
		WeatherViewModel viewModel = new WeatherViewModel (){city = UserPrefSettings.Location};
        public WeatherPage()
        {
            InitializeComponent();

			grid.Padding = new Thickness (0, 0, 50, 0);

			if (Device.OS == TargetPlatform.Android)
				grid.Padding = new Thickness (10, 0, 10, 0);
			
			BindingContext = viewModel;

			GetWeatherButton.Clicked += async (sender, e) => {
				getWeather ();
				await direction.RotateTo (viewModel.windDirection, 800, Easing.CubicInOut);
			};

			getWeather ();
        }

		async void getWeather ()
		{
			viewModel.description = "Fetching...";
			await ApiHelper.getWeather (viewModel);
        }

    }
}
