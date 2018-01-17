import {County} from "./County";
import {Role} from "./Role";

export class CircuitCourtCase {
	opponentName: string;
	judgeLastName: string;
	judgeFirstName: string;
	role: Role;
	circuitCourtCaseNumber: string;
    county?: County;

	constructor() {
		this.opponentName = "";
		this.judgeLastName = "";
		this.judgeFirstName = "";
		this.role = Role.Plaintiff;
        this.circuitCourtCaseNumber = "";
	}
}