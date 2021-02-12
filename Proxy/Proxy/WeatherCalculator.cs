using System.Threading;

namespace Proxy
{
    class WeatherCalculator : IWeatherCalculator
    {
        public double CalcClouds()
        {
            Thread.Sleep(1000);
            return 12;
        }

        public double CalcWind()
        {
            Thread.Sleep(2000);
            return 4;
        }
    }
}
