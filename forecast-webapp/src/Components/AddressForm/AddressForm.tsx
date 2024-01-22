import React, { useState } from "react";
import Address from "../Models/AddressModel";

interface AddressFormProps {
  onAddressSubmit: (address: Address) => void;
}

const AddressForm: React.FC<AddressFormProps> = (props: AddressFormProps) => {
  const [formData, setFormData] = useState<Address>({
    Street: "",
    City: "",
    StateCode: "",
    ZipCode: "",
  });

  const handleInputChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = () => {
    props.onAddressSubmit(formData);
  };

  return (
    <section className="address-form">
      <div className="flex column-container">
        <div className="flex column-container">
          <label htmlFor="streetInput">Street Address</label>
          <input
            className="required"
            id="streetInput"
            type="text"
            name="Street"
            value={formData.Street}
            placeholder="4600 Silver Hill Rd"
            onChange={handleInputChange}
          />
        </div>
        <div className="flex column-container">
          <label htmlFor="cityInput">City</label>
          <input
            id="cityInput"
            type="text"
            name="City"
            value={formData.City}
            placeholder="Washington"
            onChange={handleInputChange}
          />
        </div>
      </div>
      <div className="flex column-container">
        <div className="flex column-container">
          <label htmlFor="stateCodeSelect">State Code:</label>
          <select
            id="stateCodeSelect"
            name="StateCode"
            value={formData.StateCode}
            onChange={handleInputChange}
          >
            <option value="">State Code</option>
            {stateCodes.map((code) => (
              <option key={code} value={code}>
                {code}
              </option>
            ))}
          </select>
        </div>
        <div className="flex column-container">
          <label htmlFor="zipCodeInput">Zip Code</label>
          <input
            id="zipCodeInput"
            type="text"
            name="ZipCode"
            value={formData.ZipCode}
            placeholder="20233"
            onChange={handleInputChange}
          />
        </div>
      </div>
      <button className="search-button" onClick={handleSubmit}>
        Search
      </button>
    </section>
  );
};

const stateCodes = [
  "AL",
  "AK",
  "AS",
  "AZ",
  "AR",
  "CA",
  "CO",
  "CT",
  "DE",
  "DC",
  "FL",
  "GA",
  "GU",
  "HI",
  "ID",
  "IL",
  "IN",
  "IA",
  "KS",
  "KY",
  "LA",
  "ME",
  "MD",
  "MH",
  "MA",
  "MI",
  "FM",
  "MN",
  "MS",
  "MO",
  "MT",
  "NE",
  "NV",
  "NH",
  "NJ",
  "NM",
  "NY",
  "NC",
  "ND",
  "MP",
  "OH",
  "OK",
  "OR",
  "PW",
  "PA",
  "PR",
  "RI",
  "SC",
  "SD",
  "TN",
  "TX",
  "UT",
  "VT",
  "VA",
  "VI",
  "WA",
  "WV",
  "WI",
  "WY",
];

export default AddressForm;
