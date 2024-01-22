using System.Text.Json.Serialization;
namespace IvanLopes.Forecast.Infra.Weather.WeatherForecastModels
{
#pragma warning disable // Disable all warnings
    public record WeatherForecast
    {
        [JsonPropertyName("@context")]
        public List<object> Context { get; init; }

        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; init; }

        [JsonPropertyName("properties")]
        public ForecastProperties Properties { get; init; }
    }

    public record Geometry
    {
        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("coordinates")]
        public List<List<List<double>>> Coordinates { get; init; }
    }

    public record ForecastProperties
    {
        [JsonPropertyName("updated")]
        public string Updated { get; init; }

        [JsonPropertyName("units")]
        public string Units { get; init; }

        [JsonPropertyName("forecastGenerator")]
        public string ForecastGenerator { get; init; }

        [JsonPropertyName("generatedAt")]
        public string GeneratedAt { get; init; }

        [JsonPropertyName("updateTime")]
        public string UpdateTime { get; init; }

        [JsonPropertyName("validTimes")]
        public string ValidTimes { get; init; }

        [JsonPropertyName("elevation")]
        public Elevation Elevation { get; init; }

        [JsonPropertyName("periods")]
        public List<ForecastPeriod> Periods { get; init; }
    }

    public record Elevation
    {
        [JsonPropertyName("unitCode")]
        public string UnitCode { get; init; }

        [JsonPropertyName("value")]
        public double Value { get; init; }
    }

    public record ForecastPeriod
    {
        [JsonPropertyName("number")]
        public int Number { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("startTime")]
        public string StartTime { get; init; }

        [JsonPropertyName("endTime")]
        public string EndTime { get; init; }

        [JsonPropertyName("isDaytime")]
        public bool IsDaytime { get; init; }

        [JsonPropertyName("temperature")]
        public int Temperature { get; init; }

        [JsonPropertyName("temperatureUnit")]
        public string TemperatureUnit { get; init; }

        [JsonPropertyName("temperatureTrend")]
        public object TemperatureTrend { get; init; }

        [JsonPropertyName("probabilityOfPrecipitation")]
        public Precipitation ProbabilityOfPrecipitation { get; init; }

        [JsonPropertyName("dewpoint")]
        public Dewpoint Dewpoint { get; init; }

        [JsonPropertyName("relativeHumidity")]
        public Humidity RelativeHumidity { get; init; }

        [JsonPropertyName("windSpeed")]
        public string WindSpeed { get; init; }

        [JsonPropertyName("windDirection")]
        public string WindDirection { get; init; }

        [JsonPropertyName("icon")]
        public string Icon { get; init; }

        [JsonPropertyName("shortForecast")]
        public string ShortForecast { get; init; }

        [JsonPropertyName("detailedForecast")]
        public string DetailedForecast { get; init; }
    }

    public record Precipitation
    {
        [JsonPropertyName("unitCode")]
        public string UnitCode { get; init; }

        [JsonPropertyName("value")]
        public int? Value { get; init; }
    }

    public record Dewpoint
    {
        [JsonPropertyName("unitCode")]
        public string UnitCode { get; init; }

        [JsonPropertyName("value")]
        public double Value { get; init; }
    }

    public record Humidity
    {
        [JsonPropertyName("unitCode")]
        public string UnitCode { get; init; }

        [JsonPropertyName("value")]
        public int Value { get; init; }
    }
#pragma warning restore // Restore warning settings
}