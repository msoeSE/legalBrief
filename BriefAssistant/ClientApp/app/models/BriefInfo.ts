import { Appellant } from "./Appellant";
import { CircuitCourtCase } from "./CircuitCourtCase";

export class BriefInfo {
    circuitCourtCase: CircuitCourtCase;
    appendixDocuments: string;
    conclusion: string;
    caseFactsStatement: string;
    argument: string;
    oralArgumentStatement: string;
    publicationStatement: string;
    issuesPresented: string;
    appellant: Appellant;
    topName: string;
    bottomName: string;
    topRole: string;
    bottomRole: string;

    constructor() {
        this.circuitCourtCase = new CircuitCourtCase();
        this.appendixDocuments = "";
        this.conclusion = "";
        this.caseFactsStatement = "";
        this.argument = "";
        this.oralArgumentStatement = "";
        this.publicationStatement = "";
        this.issuesPresented = "";
        this.appellant = new Appellant();
        this.topName = "";
        this.bottomName = "";
        this.topRole = "";
        this.bottomRole = "";
    }
}