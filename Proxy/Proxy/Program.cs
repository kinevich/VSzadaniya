using System;
using System.IO;
using System.Linq;

namespace Proxy
{
    class Program
    {
        static void Main(string[] args)
        { 
            IWeatherCalculator weatherCalculator = new WeatherCalculator();

            string[] lines = File.ReadAllLines(@"E:\config.txt");

            if (lines.Contains("console"))
                weatherCalculator = new ConcoleLoggerWeatherCalculator(weatherCalculator);

            if (lines.Contains("file"))
                weatherCalculator = new FileLoggerWeatherCalculator(weatherCalculator);

            CalcTemperature(weatherCalculator);
        }

        private static double CalcTemperature(IWeatherCalculator weatherCalculator)
        {
            var predictor = new TemperaturePredictor(weatherCalculator);
            return predictor.CalcTemperature();
        }
    }
}
