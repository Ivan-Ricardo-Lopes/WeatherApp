import { DayOfWeek, DayPeriod, WeatherType } from "../Enums/Enums";

export interface ForecastApiResponse {
  days: ForecastDayDto[];
  currentPeriod: ForecastPeriodDto;
}

export interface ForecastDayDto {
  isToday: boolean;
  dayOfWeek: DayOfWeek;
  forecastPeriods: ForecastPeriodDto[];
}

export interface ForecastPeriodDto {
  name: string;
  dayPeriod: DayPeriod;
  weatherType: WeatherType;
  description: string;
  temperature: number;
  temperatureUnit: string;
  windDirection: string;
  windSpeed: string;
  relativeHumidityInPercentage: number;
}

export interface AddressInputDto {
  street: string;
  city?: string;
  stateCode?: string;
  zipCode?: string;
}

const API_URL = process.env.FORECAST_API_URL || "https://localhost:7032/api";

class GetForecastByAddress {
  public static async execute(
    address: AddressInputDto
  ): Promise<ForecastApiResponse | null> {
    try {
      const queryString = Object.entries(address)
        .map(([key, value]) => `${key}=${encodeURIComponent(value)}`)
        .join("&");

      const url = `${API_URL}/WeatherForecast?${queryString}`;

      const response = await fetch(url);

      if (!response.ok) {
        if (response.status === 400) {
          const errorResponse = await response.json();
          const errorMessages = extractErrorMessages(errorResponse);
          alert(`Validation errors: ${errorMessages.join(", ")}`);
        } else {
          alert(
            `An error occurred while fetching weather data. Status: ${response.status}`
          );
        }
        return null;
      }

      const data = await response.json();
      return data as ForecastApiResponse;
    } catch (error) {
      console.error(error);
      alert("An error occurred while fetching weather data.");
      return null;
    }
  }
}

function extractErrorMessages(errorResponse: any) {
  if (Array.isArray(errorResponse)) {
    return errorResponse.map((error) => error.message);
  } else if (errorResponse.errors) {
    return Object.values(errorResponse.errors).flat();
  } else {
    return [errorResponse.message || "Unknown error"];
  }
}

// Export service
export { GetForecastByAddress };
