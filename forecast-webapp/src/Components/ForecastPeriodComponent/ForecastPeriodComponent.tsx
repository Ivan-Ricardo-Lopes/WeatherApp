import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import IconMapper from "../../Utils/IconMapper";
import ForecastPeriod from "../Models/ForecastPeriod";

export interface ForecastPeriodProps {
  period: ForecastPeriod;
}

const ForecastPeriodComponent: React.FC<ForecastPeriodProps> = (
  props: ForecastPeriodProps
) => {
  return (
    <div className="forecast-period flex column-container">
      <div>{props.period.name}</div>
      <div className="row-container">
        <span className="temperature-text">
          {props.period.temperature}&deg;
        </span>
        <span>{props.period.temperatureUnit}</span>
      </div>
      <div>
        <FontAwesomeIcon
          icon={IconMapper(props.period.dayPeriod, props.period.weatherType)}
          className="weather-icon"
        />
      </div>
      <p>{props.period.description}</p>
    </div>
  );
};

export default ForecastPeriodComponent;
