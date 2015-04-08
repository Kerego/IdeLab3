using CrossWeather.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossWeather
{
    class Settings : CommonINotify
    {

        private bool _useGeoLocation;

        public bool useGeoLocation
        {
            get { return _useGeoLocation; }
            set
            {
                _useGeoLocation = value;
                OnPropertyChanged("useGeoLocation");
            }
        }


        
        

        public Settings()
        {
            useGeoLocation = true;
        }
    }
}
