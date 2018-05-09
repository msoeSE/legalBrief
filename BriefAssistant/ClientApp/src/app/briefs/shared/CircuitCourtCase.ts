import {County} from "./County";
import {Role} from "./Role";

export class CircuitCourtCase {
  opponentName: string;
  clientName: string;
	judgeLastName: string;
	judgeFirstName: string;
	role: Role;
	caseNumber: string;
	county: County;

	constructor() {
		this.opponentName = "";
		this.judgeLastName = "";
		this.judgeFirstName = "";
		this.role = Role.Plaintiff;
    this.caseNumber = "";
	  this.clientName = "";
	}
}
