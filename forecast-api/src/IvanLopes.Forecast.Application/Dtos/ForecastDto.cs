using IvanLopes.Forecast.Application.Enums;

namespace IvanLopes.Forecast.Application.Dtos
{
    public class ForecastDto
    {
        public List<ForecastDayDto> Days { get; set; } = new();
        public ForecastPeriodDto? CurrentPeriod { get; set; } = default!;
    }

    public class ForecastDayDto
    {
        public bool IsToday { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<ForecastPeriodDto> ForecastPeriods { get; set; } = new();
    }

    public class ForecastPeriodDto
    {
        public string Name { get; set; } = default!;
        public DayPeriod DayPeriod { get; set; }
        public WeatherType WeatherType { get; set; }
        public string Description { get; set; } = default!;
        public double Temperature { get; set; }
        public string TemperatureUnit { get; set; } = default!;
        public string WindDirection { get; set; } = default!;
        public string WindSpeed { get; set; } = default!;
        public int RelativeHumidityInPercentage { get; set; }
    }
}
