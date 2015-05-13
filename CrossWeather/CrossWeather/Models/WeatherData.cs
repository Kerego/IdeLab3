
namespace CrossWeather
{
    public class WeatherData
    {
        public Weather[] weather { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public Sys sys { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
        public int humidity { get; set; }
        public float pressure { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
    }

    public class Sys
    {
        public string country { get; set; }
    }

    public class Wind
    {
        public float speed { get; set; }
        public float gust { get; set; }
        public float deg { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}
