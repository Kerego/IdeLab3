namespace CrossWeather
{
	public class WeatherViewModel: CommonINotify
	{
		public WeatherViewModel ()
		{
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
		string _country;
		public string country {
			get
			{
				return _country;
			}
			set
			{
				_country = value;
				OnPropertyChanged ("country");
			}
		}


		string _temperature;
		public string temperature {
			get
			{
				return _temperature;
			}
			set
			{
				_temperature = value + "°C";
				OnPropertyChanged ("temperature");
			}
		}

		string _description;
		public string description {
			get
			{
				return _description;
			}
			set
			{
				_description = value;
				OnPropertyChanged ("description");
			}
		}

		string _icon;
		public string icon {
			get
			{
				return _icon;
			}
			set
			{
				_icon = "Icon" + value + ".png";
				OnPropertyChanged ("icon");
			}
		}

		string _windSpeed;
		public string windSpeed {
			get
			{
				return _windSpeed;
			}
			set
			{
				_windSpeed = string.Format ("Wind speed: {0}m/s", value);
				OnPropertyChanged ("windSpeed");
			}
		}

		float _windDirection;
		public float windDirection {
			get
			{
				return _windDirection;
			}
			set
			{
				_windDirection = value;
				OnPropertyChanged ("windDirection");
			}
		}



	}
}

