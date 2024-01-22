import React from "react";
import ForecastPeriod from "../Models/ForecastPeriod";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import IconMapper from "../../Utils/IconMapper";
import Address from "../Models/AddressModel";

interface TodayForecastDetailProps {
  currentPeriod: ForecastPeriod;
  address: Address;
}

const TodayForecastDetail: React.FC<TodayForecastDetailProps> = ({
  currentPeriod,
  address,
}) => {
  const addressesParts = [address?.Street, address?.City, address?.StateCode];
  const addressString = `${addressesParts.filter((part) => part).join(", ")} ${
    address?.ZipCode
  }`.trim();

  return (
    <div className="today-forecast-detail flex column-container">
      <h2>{addressString}</h2>
      <div className="flex row-container">
        <div>
          <span className="temperature-text">
            {currentPeriod.temperature}&deg;
          </span>
          <span>{currentPeriod.temperatureUnit}</span>
        </div>
        <div className="flex column-container">
          <FontAwesomeIcon
            icon={IconMapper(
              currentPeriod.dayPeriod,
              currentPeriod.weatherType
            )}
            className="weather-icon"
          />
          <p>{currentPeriod.description}</p>
        </div>
        <div className="flex column-container">
          <FontAwesomeIcon icon={"wind"} className="weather-icon" />
          <p>
            {currentPeriod.windSpeed} {currentPeriod.windDirection}
          </p>
        </div>
        <div className="flex column-container">
          <FontAwesomeIcon icon={"water"} className="weather-icon" />
          <p>{currentPeriod.relativeHumidityInPercentage} %</p>
        </div>
      </div>
    </div>
  );
};
export default TodayForecastDetail;
