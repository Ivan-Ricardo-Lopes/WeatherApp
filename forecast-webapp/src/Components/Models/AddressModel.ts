class Address {
  constructor(
    street: string,
    city: string,
    stateCode: string,
    zipCode: string
  ) {
    this.Street = street;
    this.City = city;
    this.StateCode = stateCode;
    this.ZipCode = zipCode;
  }

  Street: string;
  City: string;
  StateCode: string;
  ZipCode: string;
}

export default Address;
