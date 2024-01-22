using System.Text.Json.Serialization;
namespace IvanLopes.Forecast.Infra.Weather.WeatherPointModels
{
#pragma warning disable // Disable all warnings
    public record WeatherPoint
    {
        [JsonPropertyName("@context")]
        public List<object> Context { get; init; }

        [JsonPropertyName("id")]
        public string Id { get; init; }

        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; init; }

        [JsonPropertyName("properties")]
        public WeatherPointProperties Properties { get; init; }
    }

    public record Geometry
    {
        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("coordinates")]
        public List<double> Coordinates { get; init; }
    }

    public record WeatherPointProperties
    {
        [JsonPropertyName("@id")]
        public string ContextId { get; init; }

        [JsonPropertyName("@type")]
        public string ContextType { get; init; }

        [JsonPropertyName("cwa")]
        public string Cwa { get; init; }

        [JsonPropertyName("forecastOffice")]
        public string ForecastOffice { get; init; }

        [JsonPropertyName("gridId")]
        public string GridId { get; init; }

        [JsonPropertyName("gridX")]
        public int GridX { get; init; }

        [JsonPropertyName("gridY")]
        public int GridY { get; init; }

        [JsonPropertyName("forecast")]
        public string Forecast { get; init; }

        [JsonPropertyName("forecastHourly")]
        public string ForecastHourly { get; init; }

        [JsonPropertyName("forecastGridData")]
        public string ForecastGridData { get; init; }

        [JsonPropertyName("observationStations")]
        public string ObservationStations { get; init; }

        [JsonPropertyName("relativeLocation")]
        public RelativeLocation RelativeLocation { get; init; }

        [JsonPropertyName("forecastZone")]
        public string ForecastZone { get; init; }

        [JsonPropertyName("county")]
        public string County { get; init; }

        [JsonPropertyName("fireWeatherZone")]
        public string FireWeatherZone { get; init; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; init; }

        [JsonPropertyName("radarStation")]
        public string RadarStation { get; init; }
    }

    public record RelativeLocation
    {
        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; init; }

        [JsonPropertyName("properties")]
        public RelativeLocationProperties Properties { get; init; }
    }

    public record RelativeLocationProperties
    {
        [JsonPropertyName("city")]
        public string City { get; init; }

        [JsonPropertyName("state")]
        public string State { get; init; }

        [JsonPropertyName("distance")]
        public Distance Distance { get; init; }

        [JsonPropertyName("bearing")]
        public Distance Bearing { get; init; }
    }

    public record Distance
    {
        [JsonPropertyName("unitCode")]
        public string UnitCode { get; init; }

        [JsonPropertyName("value")]
        public double Value { get; init; }
    }
#pragma warning restore // Restore warning settings
}