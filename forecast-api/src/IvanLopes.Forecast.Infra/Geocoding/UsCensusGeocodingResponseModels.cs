using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace IvanLopes.Forecast.Infra.Weather.Geocoding.OneLineAddressResponseModels
{
#pragma warning disable // Disable all warnings
    public record OneLineAddressResponse
    {
        [JsonPropertyName("result")]
        public Result Result { get; init; }
    }

    public record Result
    {
        [JsonPropertyName("input")]
        public Input Input { get; init; }

        [JsonPropertyName("addressMatches")]
        public List<AddressMatch> AddressMatches { get; init; }
    }

    public record Input
    {
        [JsonPropertyName("address")]
        public Address Address { get; init; }

        [JsonPropertyName("benchmark")]
        public Benchmark Benchmark { get; init; }
    }

    public record Address
    {
        [JsonPropertyName("zip")]
        public string Zip { get; init; }

        [JsonPropertyName("city")]
        public string City { get; init; }

        [JsonPropertyName("street")]
        public string Street { get; init; }

        [JsonPropertyName("state")]
        public string State { get; init; }
    }

    public record Benchmark
    {
        [JsonPropertyName("isDefault")]
        public bool IsDefault { get; init; }

        [JsonPropertyName("benchmarkDescription")]
        public string BenchmarkDescription { get; init; }

        [JsonPropertyName("id")]
        public string Id { get; init; }

        [JsonPropertyName("benchmarkName")]
        public string BenchmarkName { get; init; }
    }

    public record AddressMatch
    {
        [JsonPropertyName("tigerLine")]
        public TigerLine TigerLine { get; init; }

        [JsonPropertyName("coordinates")]
        public Coordinates Coordinates { get; init; }

        [JsonPropertyName("addressComponents")]
        public AddressComponents AddressComponents { get; init; }

        [JsonPropertyName("matchedAddress")]
        public string WatchedAddress { get; init; }
    }

    public record TigerLine
    {
        [JsonPropertyName("side")]
        public string Side { get; init; }

        [JsonPropertyName("tigerLineId")]
        public string TigerLineId { get; init; }
    }

    public record Coordinates
    {
        [JsonPropertyName("x")]
        public double X { get; init; }

        [JsonPropertyName("y")]
        public double Y { get; init; }
    }

    public record AddressComponents
    {
        [JsonPropertyName("zip")]
        public string Zip { get; init; }

        [JsonPropertyName("streetName")]
        public string StreetName { get; init; }

        [JsonPropertyName("preType")]
        public string PreType { get; init; }

        [JsonPropertyName("city")]
        public string City { get; init; }

        [JsonPropertyName("preDirection")]
        public string PreDirection { get; init; }

        [JsonPropertyName("suffixDirection")]
        public string SuffixDirection { get; init; }

        [JsonPropertyName("fromAddress")]
        public string FromAddress { get; init; }

        [JsonPropertyName("state")]
        public string State { get; init; }

        [JsonPropertyName("suffixType")]
        public string SuffixType { get; init; }

        [JsonPropertyName("toAddress")]
        public string ToAddress { get; init; }

        [JsonPropertyName("suffixQualifier")]
        public string SuffixQualifier { get; init; }

        [JsonPropertyName("preQualifier")]
        public string PreQualifier { get; init; }
    }
#pragma warning restore // Restore warning settings
}