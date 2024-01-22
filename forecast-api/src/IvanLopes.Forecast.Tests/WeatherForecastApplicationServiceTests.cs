using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using IvanLopes.Forecast.Application.ApplicationServices.WeatherForecast;
using IvanLopes.Forecast.Application.Dtos;
using IvanLopes.Forecast.Application.InfraServices;
using Moq;

public class WeatherForecastApplicationServiceTests
{
    [Fact]
    public async Task GetWeatherForecastAsync_WithValidAddress_ShouldReturnForecastDto()
    {
        // Arrange
        var addressInput = new AddressInput
        {
            Street = "123 Main St",
            City = "City",
            StateCode = "ST",
            ZipCode = "12345"
        };

        var geocodingServiceMock = new Mock<IGeocodingService>();
        geocodingServiceMock.Setup(service => service.GetCoordinatesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new CoordinatesDto(1, 1));

        var weatherServiceMock = new Mock<IWeatherService>();
        weatherServiceMock.Setup(service => service.GetForecastAsync(It.IsAny<CoordinatesDto>()))
            .ReturnsAsync(new ForecastDto());

        var cacheServiceMock = new Mock<ICacheService>();
        cacheServiceMock.Setup(service => service.Get<ForecastDto>(It.IsAny<string>()))
            .Returns(new ForecastDto());

        var validatorMock = new Mock<IValidator<AddressInput>>();
        validatorMock.Setup(validator => validator.Validate(addressInput))
            .Returns(new ValidationResult());

        var service = new WeatherForecastApplicationService(
            geocodingServiceMock.Object,
            weatherServiceMock.Object,
            cacheServiceMock.Object,
            validatorMock.Object);

        // Act
        var result = await service.GetWeatherForecastAsync(addressInput);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetWeatherForecastAsync_WithInvalidAddress_ShouldReturnValidationFailure()
    {
        // Arrange
        var addressInput = new AddressInput();

        var validatorMock = new Mock<IValidator<AddressInput>>();
        validatorMock.Setup(validator => validator.Validate(addressInput))
            .Returns(new ValidationResult(new[] { new ValidationFailure("Address", "Address is required") }));

        var service = new WeatherForecastApplicationService(
            Mock.Of<IGeocodingService>(),
            Mock.Of<IWeatherService>(),
            Mock.Of<ICacheService>(),
            validatorMock.Object);

        // Act
        var result = await service.GetWeatherForecastAsync(addressInput);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Message.Should().Be("Address is required");
    }

    [Fact]
    public async Task GetWeatherForecastAsync_WithCachedResult_ShouldReturnCachedForecastDto()
    {
        // Arrange
        var addressInput = new AddressInput
        {
            Street = "123 Main St",
            City = "City",
            StateCode = "ST",
            ZipCode = "12345"
        };

        var cachedForecast = new ForecastDto();

        var geocodingServiceMock = new Mock<IGeocodingService>();
        geocodingServiceMock.Setup(service => service.GetCoordinatesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new CoordinatesDto(1, 2));

        var weatherServiceMock = new Mock<IWeatherService>();
        weatherServiceMock.Setup(service => service.GetForecastAsync(It.IsAny<CoordinatesDto>()))
            .ReturnsAsync(new ForecastDto());

        var cacheServiceMock = new Mock<ICacheService>();
        cacheServiceMock.Setup(service => service.Get<ForecastDto>(It.IsAny<string>()))
            .Returns(cachedForecast);

        var validatorMock = new Mock<IValidator<AddressInput>>();
        validatorMock.Setup(validator => validator.Validate(addressInput))
            .Returns(new ValidationResult());

        var service = new WeatherForecastApplicationService(
            geocodingServiceMock.Object,
            weatherServiceMock.Object,
            cacheServiceMock.Object,
            validatorMock.Object);

        // Act
        var result = await service.GetWeatherForecastAsync(addressInput);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeSameAs(cachedForecast);
    }

    [Fact]
    public async Task GetWeatherForecastAsync_WithNoCoordinatesFound_ShouldReturnFailure()
    {
        // Arrange
        var addressInput = new AddressInput
        {
            Street = "123 Main St",
            City = "City",
            StateCode = "ST",
            ZipCode = "12345"
        };

        var geocodingServiceMock = new Mock<IGeocodingService>();
        geocodingServiceMock.Setup(service => service.GetCoordinatesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((CoordinatesDto?)null);

        var validatorMock = new Mock<IValidator<AddressInput>>();
        validatorMock.Setup(validator => validator.Validate(addressInput))
            .Returns(new ValidationResult());

        var service = new WeatherForecastApplicationService(
            geocodingServiceMock.Object,
            Mock.Of<IWeatherService>(),
            Mock.Of<ICacheService>(),
            validatorMock.Object);

        // Act
        var result = await service.GetWeatherForecastAsync(addressInput);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Message.Should().Be("Forecast not found for this address");
    }

    [Fact]
    public async Task GetWeatherForecastAsync_WithNoForecastAvailable_ShouldReturnFailure()
    {
        // Arrange
        var addressInput = new AddressInput
        {
            Street = "123 Main St",
            City = "City",
            StateCode = "ST",
            ZipCode = "12345"
        };

        var geocodingServiceMock = new Mock<IGeocodingService>();
        geocodingServiceMock.Setup(service => service.GetCoordinatesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new CoordinatesDto(1, 2));

        var weatherServiceMock = new Mock<IWeatherService>();
        weatherServiceMock.Setup(service => service.GetForecastAsync(It.IsAny<CoordinatesDto>()))
            .ReturnsAsync((ForecastDto?)null);

        var validatorMock = new Mock<IValidator<AddressInput>>();
        validatorMock.Setup(validator => validator.Validate(addressInput))
            .Returns(new ValidationResult());

        var service = new WeatherForecastApplicationService(
            geocodingServiceMock.Object,
            weatherServiceMock.Object,
            Mock.Of<ICacheService>(),
            validatorMock.Object);

        // Act
        var result = await service.GetWeatherForecastAsync(addressInput);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Message.Should().Be("Forecast not available for this address");
    }
}
