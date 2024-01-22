using FluentResults;
using IvanLopes.Forecast.Application.ApplicationServices.WeatherForecast;
using IvanLopes.Forecast.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IvanLopes.Forecast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastApplicationService _weatherForecastApplicationService;

        public WeatherForecastController(IWeatherForecastApplicationService weatherForecastApplicationService)
        {
            this._weatherForecastApplicationService = weatherForecastApplicationService;
        }

        /// <summary>
        /// Get weather forecast for a specific address.
        /// </summary>
        /// <param name="address">The address for which to retrieve the forecast.</param>
        /// <returns>
        /// Returns a JSON object containing the weather forecast for the given address.
        /// If successful, returns a 200 OK response with the forecast data.
        /// If there are validation errors, returns a 400 Bad Request response with error details.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(Result<ForecastDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] AddressInput address)
        {
            Result<ForecastDto> result = await _weatherForecastApplicationService.GetWeatherForecastAsync(address);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.ValueOrDefault);
        }
    }
}
