using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossWeather
{
    public interface IGeolocation
    {
        Task<Tuple<double,double>> getCurrentPosition();
    }
}
