using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using CrossWeather.Droid;
using Android.Locations;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Geolocation_Android))]
namespace CrossWeather.Droid
{
    class Geolocation_Android : Java.Lang.Object, ILocationListener, IGeolocation
    {
        Tuple<double,double> position;

        public Geolocation_Android()
        {

        }

        public async Task<Tuple<double,double>> getCurrentPosition()
        {

            var wrapper = new Android.Content.ContextWrapper(Forms.Context);

            locationManager = (LocationManager)wrapper.GetSystemService(Context.LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                locationProvider = acceptableLocationProviders.First();
                locationManager.RequestLocationUpdates(locationProvider, 300, 10, this);

            }
            else
            {
                locationProvider = String.Empty;
            }


            currentLocation = locationManager.GetLastKnownLocation(locationProvider);

            if (currentLocation != null)
            {
                position = new Tuple<double, double>(currentLocation.Latitude, currentLocation.Longitude);
            }

            return position;
        }


        Android.Locations.Location currentLocation;
        LocationManager locationManager;

        string locationProvider;

        public void OnLocationChanged(Android.Locations.Location location)
        {
            currentLocation = location;
            if (currentLocation == null)
            {

            }
            else
            {
                position = new Tuple<double,double>(currentLocation.Latitude, currentLocation.Longitude);
                MessagingCenter.Send<IGeolocation, Tuple<double,double>>(this, "LocationChanged", position);
            }
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            Console.WriteLine("{0}, {1}", provider, status);
        }


        public void StopLocationService()
        {
            locationManager.RemoveUpdates(this);
        }

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        //    Log.Debug(LogTag, "Listening for location updates using " + _locationProvider + ".");
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    _locationManager.RemoveUpdates(this);
        //    Log.Debug(LogTag, "No longer listening for location updates.");
        //}


    }
}