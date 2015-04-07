using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossWeather
{
    class Settings
    {
        public bool useGeoLocation { get; set;}

        public Settings()
        {
            useGeoLocation = false;
        }
    }
}
