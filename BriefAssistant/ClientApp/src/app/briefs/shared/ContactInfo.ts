import { Address } from "./Address";

export class ContactInfo {
  phone: string;
  email: string;
  address: Address;
  name: string;
  barId: string;

  constructor() {
    this.phone = "";
    this.email = "";
    this.address = new Address();
    this.name = "";
    this.barId = "";
  }
}
