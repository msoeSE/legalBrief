import { State } from './State';

export class Address {
	zip: string;
	state: State;
	city: string;
	street2: string;
	street: string;

	constructor() {
		this.zip = "";
		this.state = State.Wisconsin;
		this.city = "";
		this.street2 = "";
		this.street = "";
	}
}