using System;
using System.Diagnostics;
using System.IO;

namespace Proxy
{
    class FileLoggerWeatherCalculator : IWeatherCalculator
    {
        private readonly IWeatherCalculator _weatherCalculator;

        public FileLoggerWeatherCalculator(IWeatherCalculator weatherCalculator)
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
            using (StreamWriter file =
                   new StreamWriter(@"E:\someFile.txt", true))
            {
                file.WriteLine($"{prefix}: {stopwatch.ElapsedMilliseconds} (elapsed milliseconds)");
            }

            return result;
        }
    }
}
