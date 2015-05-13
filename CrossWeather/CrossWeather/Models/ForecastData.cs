namespace CrossWeather.Forecast
{
	public class ForecastData
	{
	    public List[] list { get; set; }
	}
    
	public class List
	{
	    public Temp temp { get; set; }
	    public Weather[] weather { get; set; }
	}

	public class Temp
	{
	    public float day { get; set; }
	    public float night { get; set; }
	}

	public class Weather
	{
	    public string icon { get; set; }
	}
}
