using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var weatherCalculator = new WeatherCalculator();
            var proxy = new FileLoggerWeatherCalculator(weatherCalculator);
            var temperaturePredictor = new TemperaturePredictor(proxy);
            Console.WriteLine(temperaturePredictor.CalcTemperature());

        }
    }

    class TemperaturePredictor
    {
        private readonly IWeatherCalculator _weatherCalculator;

        public TemperaturePredictor(IWeatherCalculator weatherCalculator)
        {
            _weatherCalculator = weatherCalculator;
        }

        public double CalcTemperature()
        {
            return _weatherCalculator.CalcWind() * _weatherCalculator.CalcClouds();;
        }
    }

    interface IWeatherCalculator
    {
        double CalcClouds();

        double CalcWind();
    }
    
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
                   new StreamWriter(@"E:\config.txt", true))
            {
                file.WriteLine($"{prefix}: {stopwatch.ElapsedMilliseconds} (elapsed milliseconds)");
            }

            return result;
        }
    }
}
