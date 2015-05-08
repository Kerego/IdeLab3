using System;
using System.Threading.Tasks;

namespace CrossWeather
{
    public interface IGeolocation
    {
        Task<Tuple<double,double>> getCurrentPosition();
    }
}
