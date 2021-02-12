namespace Proxy
{
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
}
