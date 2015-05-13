using Xamarin.Forms;

namespace CrossWeather
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
			CityData.Text = UserPrefSettings.Location;

			grid.Padding = new Thickness (0, 0, 50, 0);

			if (Device.OS == TargetPlatform.Android)
				grid.Padding = new Thickness (20, 0, 20, 0);

			SaveButton.Clicked += (s, e) => {
				UserPrefSettings.Location = CityData.Text;
			};
			GPSButton.Clicked += async (sender, e) => {
				CityData.Text = await ApiHelper.getLocation();
			};

        }
    }
}
