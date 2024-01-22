using FluentResults;
using FluentValidation;
using IvanLopes.Forecast.Application.Dtos;
using IvanLopes.Forecast.Application.InfraServices;

namespace IvanLopes.Forecast.Application.ApplicationServices.WeatherForecast
{
    public class WeatherForecastApplicationService : IWeatherForecastApplicationService
    {
        private readonly IGeocodingService _geocodingService;
        private readonly IWeatherService _weatherService;
        private readonly ICacheService _cacheService;
        private readonly IValidator<AddressInput> _validator;

        public WeatherForecastApplicationService(IGeocodingService geocodingService,
            IWeatherService weatherService,
            ICacheService cacheService,
            IValidator<AddressInput> validator)
        {
            _geocodingService = geocodingService ?? throw new ArgumentNullException(nameof(geocodingService));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<ForecastDto>> GetWeatherForecastAsync(AddressInput address)
        {
            var validationResult = _validator.Validate(address);
            if (!validationResult.IsValid)
            {
                return Result.Fail<ForecastDto>(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            var formattedAddress = FormatAddress(address);

            var cacheKey = $"Forecast_{formattedAddress}";
            var cachedResult = _cacheService.Get<ForecastDto>(cacheKey);
            if (cachedResult != null)
            {
                return Result.Ok(cachedResult);
            }

            var coordinates = await _geocodingService.GetCoordinatesAsync(address.Street, address.City, address.StateCode, address.ZipCode).ConfigureAwait(false);
            if (coordinates == null)
            {
                return Result.Fail<ForecastDto>("Forecast not found for this address");
            }

            var forecast = await _weatherService.GetForecastAsync(coordinates).ConfigureAwait(false);
            if (forecast == null)
            {
                return Result.Fail<ForecastDto>("Forecast not available for this address");
            }

            _cacheService.Set(cacheKey, forecast, TimeSpan.FromHours(1));
            return Result.Ok(forecast);
        }

        private string FormatAddress(AddressInput address)
        {
            var formattedAddress = $"{(string.IsNullOrWhiteSpace(address.Street) ? "" : address.Street.Trim())}" +
                      $"{(!string.IsNullOrWhiteSpace(address.City) ? $", {address.City.Trim()}" : "")}" +
                      $"{(!string.IsNullOrWhiteSpace(address.StateCode) ? $" {address.StateCode.Trim()}" : "")}" +
                      $"{(!string.IsNullOrWhiteSpace(address.ZipCode) ? $" {address.ZipCode.Trim()}" : "")}";

            return formattedAddress;
        }
    }
}
