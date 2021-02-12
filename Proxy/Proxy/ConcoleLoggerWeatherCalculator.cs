using System;
using System.Diagnostics;

namespace Proxy
{
    class ConcoleLoggerWeatherCalculator : IWeatherCalculator
    {
        private readonly IWeatherCalculator _weatherCalculator;

        public ConcoleLoggerWeatherCalculator(IWeatherCalculator weatherCalculator)
        {
            _weatherCalculator = weatherCalculator;
        }

        public double CalcClouds()
        {
            return Log("CalcClouds", _weatherCalculator.CalcClouds);
        }

        public double CalcWind()
        {
            return Log("CalcWind", _weatherCalculator.CalcWind);
        }

        public double Log(string prefix, Func<double> action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = action();

            stopwatch.Stop();
            Console.WriteLine($"{prefix}: {stopwatch.ElapsedMilliseconds} (elapsed milliseconds)");

            return result;
        }
    }
}
