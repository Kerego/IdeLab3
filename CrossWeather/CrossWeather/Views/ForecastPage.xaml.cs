using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CrossWeather
{
	public partial class ForecastPage : ContentPage
	{
		ForecastViewModel viewModel = new ForecastViewModel() {city = UserPrefSettings.Location};
		public ForecastPage ()
		{
			InitializeComponent ();

			grid.Padding = new Thickness (0, 0, 50, 0);

			if (Device.OS == TargetPlatform.Android)
				grid.Padding = new Thickness (10, 10, 10, 0);

			BindingContext = viewModel;
			listView.ItemsSource = viewModel.data;

			getForecastButton.Clicked += async(sender, e) => {
				getForecastButton.Text = "...";
				viewModel.data.Clear();
				await ApiHelper.getForecast (viewModel);
				getForecastButton.Text = "Get";
			};
		}
	}
}

