import { Address } from "./Address";

export class Appellant {
	phone: string;
	email: string;
	address: Address;
	name: string;

	constructor() {
		this.phone = "";
		this.email = "";
		this.address = new Address();
		this.name = "";
	}
}