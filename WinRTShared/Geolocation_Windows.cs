using CrossWeather;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using WinRTShared;

[assembly: Xamarin.Forms.Dependency(typeof(Geolocation_Windows))]
namespace WinRTShared
{
    public class Geolocation_Windows : IGeolocation
    {
        public Geolocation_Windows()
        {

        }



        public async Task<Tuple<double,double>> getCurrentPosition()
        {
            Geolocator geolocator = new Geolocator();

            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoPosition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10));
                return new Tuple<double, double>(geoPosition.Coordinate.Point.Position.Latitude, geoPosition.Coordinate.Point.Position.Longitude);
            }

            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine("Bad permissions");
            }

            return new Tuple<double,double>(0,0);
        }
    }
}
