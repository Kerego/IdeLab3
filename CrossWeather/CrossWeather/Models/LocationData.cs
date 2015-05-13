namespace CrossWeather
{
	public class LocationData
	{
		public Result[] results { get; set; }
	}

    public class Result
    {
        public Address_Components[] address_components { get; set; }
    }

    public class Address_Components
    {
        public string short_name { get; set; }
        public string[] types { get; set; }
    }
}





