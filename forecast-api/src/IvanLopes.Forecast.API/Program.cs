
using HealthChecks.UI.Client;
using IvanLopes.Forecast.API.HealthChecks;
using IvanLopes.Forecast.API.Middlewares;
using IvanLopes.Forecast.Infra.Caching;
using IvanLopes.Forecast.Infra.Geocoding;
using IvanLopes.Forecast.Infra.Weather;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Polly;
using System.Threading.RateLimiting;
using Polly.Extensions.Http;
using IvanLopes.Forecast.Application.InfraServices;
using IvanLopes.Forecast.Application.ApplicationServices.WeatherForecast;
using FluentValidation;

namespace IvanLopes.Forecast.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /// Settings
            var usCensusGeocodingSettingsSection = builder.Configuration.GetSection("UsCensusGeocodingSettings");
            var usCensusGeocodingSettings = usCensusGeocodingSettingsSection.Get<UsCensusGeocodingSettings>();
            builder.Services.AddSingleton(usCensusGeocodingSettings!);

            var usNationalWeatherSettingsSection = builder.Configuration.GetSection("UsNationalWeatherSettings");
            var usNationalWeatherServiceSettings = usNationalWeatherSettingsSection.Get<UsNationalWeatherSettings>();
            builder.Services.AddSingleton(usNationalWeatherServiceSettings!);

            // Services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IGeocodingService, UsCensusGeocodingService>();
            builder.Services.AddScoped<IWeatherService, UsNationalWeatherService>();
            builder.Services.AddScoped<IWeatherForecastApplicationService, WeatherForecastApplicationService>();

            /// Http Clients
            builder.Services.AddHttpClient(nameof(IWeatherService), client =>
            {
                client.DefaultRequestHeaders.Add("User-Agent", "(ivanlopesapi, ivanricardolopes@gmail.com)");

            })
                .SetHandlerLifetime(TimeSpan.FromMinutes(10))
                .AddPolicyHandler(GetRetryPolicy());

            builder.Services.AddHttpClient("Default")
                .SetHandlerLifetime(TimeSpan.FromMinutes(10))
                .AddPolicyHandler(GetRetryPolicy());

            /// Exception Handling
            builder.Services.AddExceptionHandler<DefaultExceptionHandler>();

            /// Validators
            builder.Services.AddValidatorsFromAssemblyContaining<IWeatherForecastApplicationService>();

            /// Caching
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<ICacheService, InMemoryCacheService>();

            /// Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "DevCorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                    });
            });

            /// Rate Limiting
            builder.Services.AddRateLimiter(config => config.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            {
                return RateLimitPartition.GetConcurrencyLimiter<string>("GlobalLimit",
                    _ => new ConcurrencyLimiterOptions()
                    {
                        PermitLimit = 1,
                        QueueProcessingOrder = QueueProcessingOrder.NewestFirst,
                        QueueLimit = 10
                    }
                    );
            }));

            /// Health Checks
            builder.Services.AddHealthChecks()
                .AddCheck<UsNationalWeatherHealthCheck>(nameof(UsNationalWeatherHealthCheck))
                .AddCheck<UsCensusGeocodingHealthCheck>(nameof(UsCensusGeocodingService));

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("DevCorsPolicy");
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRateLimiter();

            app.MapControllers();

            app.UseExceptionHandler(_ => { });

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.Run();
        }
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
