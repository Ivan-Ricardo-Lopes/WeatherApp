import { useState } from "react";
import WeekForecast from "../WeekForecast/WeekForecast";
import AddressForm from "../AddressForm/AddressForm";
import ForecastDay from "../Models/ForecastDay";
import Address from "../Models/AddressModel";
import { GetForecastByAddress } from "../../Api/GetForecastByAddress";
import TodayForecastDetail from "../TodayForecastDetail/TodayForecastDetail";
import ForecastPeriod from "../Models/ForecastPeriod";

const ForecastPage = () => {
  const [displayedForecast, setDisplayedForecast] = useState<ForecastDay[]>([]);
  const [currentPeriodForecast, setCurrentPeriodForecast] =
    useState<ForecastPeriod>();
  const [displayAddress, setAddress] = useState<Address | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  const getForecast = async (address: Address) => {
    setAddress(address);
    setIsLoading(true);

    await GetForecastByAddress.execute({
      street: address.Street,
      city: address.City,
      stateCode: address.StateCode,
      zipCode: address.ZipCode,
    }).then((response) => {
      setIsLoading(false);
      if (response && response?.days?.length > 0) {
        setDisplayedForecast(response.days);
        setCurrentPeriodForecast(response.currentPeriod);
      }
    });
  };

  return (
    <div className="forecast-page flex column-container">
      {isLoading && (
        <div className="loading-overlay">
          <div className="loading-message">Loading...</div>
        </div>
      )}
      <AddressForm
        onAddressSubmit={(address: Address) => getForecast(address)}
      />
      {currentPeriodForecast && displayAddress && (
        <TodayForecastDetail
          address={displayAddress}
          currentPeriod={currentPeriodForecast}
        />
      )}
      {displayedForecast.length > 0 && (
        <WeekForecast days={displayedForecast} />
      )}
    </div>
  );
};

export default ForecastPage;
