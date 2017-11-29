import {County} from "./County";
import {Role} from "./Role";

export class CircuitCourtCase {
	opponentName: string;
	judgeLastName: string;
	judgeFirstName: string;
	role: Role;
	caseNumber: string;
    county?: County;
    opponentRole: Role;

	constructor() {
		this.opponentName = "";
		this.judgeLastName = "";
		this.judgeFirstName = "";
		this.role = Role.Plaintiff;
        this.caseNumber = "";
	    this.opponentRole = Role.Defendent;
	}
}