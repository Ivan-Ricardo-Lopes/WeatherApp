using FluentValidation;

namespace IvanLopes.Forecast.Application.ApplicationServices.WeatherForecast
{
    public class AddressInputValidator : AbstractValidator<AddressInput>
    {
        public AddressInputValidator()
        {
            RuleFor(x => x.Street)
                .NotEmpty()
                .WithMessage("Street Address is required")
                .MaximumLength(100)
                .WithMessage("Street Address cannot be longer than 100 characters");

            RuleFor(x => x.City)
                .MaximumLength(50)
                .WithMessage("City cannot be longer than 50 characters");

            RuleFor(x => x.StateCode)
                .Length(2)
                .WithMessage("State code cannot be longer than 2 characters");

            RuleFor(x => x.ZipCode)
                .MaximumLength(15)
                .WithMessage("Zip code cannot be longer than 15 characters");
        }
    }
}
