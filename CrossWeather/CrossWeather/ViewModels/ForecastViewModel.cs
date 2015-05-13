using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrossWeather
{
	public class ForecastViewModel : CommonINotify
	{
		public ForecastViewModel()
		{
			data = new ObservableCollection<ForecastDay>();
		}

		string _city;
		public string city {
			get
			{
				return _city;
			}
			set
			{
				_city = value;
				OnPropertyChanged ("city");
			}
		}

		public ObservableCollection <ForecastDay> data;

		public class ForecastDay
		{
			public string temp { get; set; }
			public string date { get; set; }
			public string icon { get; set; }
		}

	}
}

