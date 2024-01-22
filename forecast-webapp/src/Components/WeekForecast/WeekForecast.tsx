import React from "react";
import "../../styles.css";
import ForecastDay from "../Models/ForecastDay";
import ForecastPeriodComponent from "../ForecastPeriodComponent/ForecastPeriodComponent";

interface ForecastDayProps {
  days: ForecastDay[];
}

const WeekForecast: React.FC<ForecastDayProps> = (props: ForecastDayProps) => {
  return (
    <section className="WeekForecast">
      {props.days.map((dayForecast, index) => (
        <div
          key={`${dayForecast.dayOfWeek}_${index}`}
          className="WeekForecastDay flex column-container"
        >
          {dayForecast.forecastPeriods.map((period, index2) => (
            <ForecastPeriodComponent
              key={`${dayForecast.dayOfWeek}_${index}_${index2}`}
              period={period}
            />
          ))}
        </div>
      ))}
    </section>
  );
};
export default WeekForecast;
