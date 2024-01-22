using IvanLopes.Forecast.Application.Dtos;
using IvanLopes.Forecast.Application.Enums;
using IvanLopes.Forecast.Application.Extensions;
using IvanLopes.Forecast.Application.InfraServices;
using IvanLopes.Forecast.Infra.Weather.WeatherForecastModels;
using IvanLopes.Forecast.Infra.Weather.WeatherPointModels;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace IvanLopes.Forecast.Infra.Weather
{
    public class UsNationalWeatherService : IWeatherService
    {
        private readonly UsNationalWeatherSettings _settings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UsNationalWeatherService> _logger;

        public UsNationalWeatherService(UsNationalWeatherSettings settings,
            IHttpClientFactory httpClientFactory,
            ILogger<UsNationalWeatherService> logger)
        {
            _settings = settings;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<ForecastDto?> GetForecastAsync(CoordinatesDto coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentException("Coordinates cannot be null.", nameof(coordinates));
            }

            HttpClient httpClient = _httpClientFactory.CreateClient(nameof(IWeatherService));

            var url = $"{_settings.ApiBaseUrl}/points/{coordinates.Y},{coordinates.X}";
            HttpResponseMessage pointsResponse = await httpClient.GetAsync(url);

            if (!pointsResponse.IsSuccessStatusCode)
            {
                if (pointsResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Forecast not found for coordinates {Y},{X}", coordinates.Y, coordinates.X);
                    return null;
                }

                _logger.LogError("Error getting grid points from UsNationalWeatherService. Status code: {StatusCode}", pointsResponse.StatusCode);
                return null;
            }

            string gridPointsContent = await pointsResponse.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(gridPointsContent))
            {
                _logger.LogError("Error getting grid points from UsNationalWeatherService. Content is null or whitespace.");
                return null;
            }

            try
            {
                var pointsObject = JsonSerializer.Deserialize<WeatherPoint>(gridPointsContent);

                if (pointsObject == null)
                {
                    return null;
                }

                var forecastDaysTask = GetForecastDaysAsync(httpClient, pointsObject.Properties.Forecast);
                var currentPeriodForecastTask = GetForecastNowAsync(httpClient, pointsObject.Properties.ForecastHourly);

                await Task.WhenAll(forecastDaysTask, currentPeriodForecastTask);

                var forecastDays = await forecastDaysTask;
                var currentPeriodForecast = await currentPeriodForecastTask;

                var forecast = new ForecastDto
                {
                    Days = forecastDays ?? new(),
                    CurrentPeriod = currentPeriodForecast
                };

                return forecast;

            }
            catch (JsonException e)
            {
                _logger.LogError(e, "Error deserializing PointsEndpointResponse from UsNationalWeatherService");
                return null;
            }
        }

        private async Task<List<ForecastDayDto>?> GetForecastDaysAsync(HttpClient httpClient, string forecastEndpoint)
        {
            var forecastResponse = await httpClient.GetAsync(forecastEndpoint);

            if (!forecastResponse.IsSuccessStatusCode)
            {
                _logger.LogError("Error getting forecast from UsNationalWeatherService. Status code: {StatusCode}", forecastResponse.StatusCode);
                return null;
            }

            string forecastContent = await forecastResponse.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(forecastContent))
            {
                _logger.LogError("Error getting forecast from UsNationalWeatherService. Content is null or whitespace.");
                return null;
            }

            var forecastObject = JsonSerializer.Deserialize<WeatherForecast>(forecastContent);
            if (forecastObject == null)
            {
                return null;
            }

            var forecastDays = forecastObject.Properties.Periods
                .GroupBy(p => DateTime.Parse(p.StartTime).DayOfWeek)
                .Select(group => new
                {
                    DayOfWeek = group.Key,
                    Periods = group.OrderBy(p => DateTime.Parse(p.StartTime)).TakeLast(2)
                })
                .Select(g => new ForecastDayDto
                {
                    DayOfWeek = g.DayOfWeek,
                    IsToday = g.Periods.Any(x => x.IsDaytime && DateTimeOffset.Parse(x.StartTime).IsToday()),
                    ForecastPeriods = g.Periods.Select(p => new ForecastPeriodDto
                    {
                        Name = p.Name,
                        Description = p.ShortForecast,
                        Temperature = p.Temperature,
                        TemperatureUnit = p.TemperatureUnit,
                        DayPeriod = p.IsDaytime ? DayPeriod.Day : DayPeriod.Night,
                        WindDirection = p.WindDirection,
                        WindSpeed = p.WindSpeed,
                        WeatherType = WeatherForecastAnalyzer.DetermineWeatherTypes(p.ShortForecast),
                        RelativeHumidityInPercentage = p.RelativeHumidity.Value
                    }).ToList()
                }).ToList();

            return forecastDays;
        }

        private async Task<ForecastPeriodDto?> GetForecastNowAsync(HttpClient httpClient, string forecastEndpoint)
        {
            var forecastResponse = await httpClient.GetAsync(forecastEndpoint);

            if (!forecastResponse.IsSuccessStatusCode)
            {
                _logger.LogError("Error getting forecast from UsNationalWeatherService. Status code: {StatusCode}", forecastResponse.StatusCode);
                return null;
            }

            string forecastContent = await forecastResponse.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(forecastContent))
            {
                _logger.LogError("Error getting forecast from UsNationalWeatherService. Content is null or whitespace.");
                return null;
            }

            var forecastObject = JsonSerializer.Deserialize<WeatherForecast>(forecastContent);
            if (forecastObject == null)
            {
                return null;
            }

            var forecastNow = forecastObject.Properties.Periods
                .Where(p => DateTimeOffset.Parse(p.StartTime).Hour == DateTimeOffset.Now.Hour && DateTimeOffset.Parse(p.StartTime).IsToday())
                .FirstOrDefault();


            return new ForecastPeriodDto
            {
                Name = forecastNow?.Name ?? string.Empty,
                Description = forecastNow?.ShortForecast ?? string.Empty,
                Temperature = forecastNow?.Temperature ?? 0,
                TemperatureUnit = forecastNow?.TemperatureUnit ?? string.Empty,
                DayPeriod = forecastNow?.IsDaytime == true ? DayPeriod.Day : DayPeriod.Night,
                WindDirection = forecastNow?.WindDirection ?? string.Empty,
                WindSpeed = forecastNow?.WindSpeed ?? string.Empty,
                WeatherType = WeatherForecastAnalyzer.DetermineWeatherTypes(forecastNow?.ShortForecast ?? string.Empty),
                RelativeHumidityInPercentage = forecastNow?.RelativeHumidity?.Value ?? default
            };
        }
    }
}
