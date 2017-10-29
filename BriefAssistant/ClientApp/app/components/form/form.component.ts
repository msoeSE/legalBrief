import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgModule } from '@angular/core';
import { Router } from '@angular/router';

import { HttpModule, URLSearchParams ,Http} from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';

interface BriefInfo {
    appellant: Appellant;
    issuesPresented: string;
    oralArgumentStatement: string;
    publicationStatement: string;
    caseFactsStatement : string;
    argument: string;
    conclusion: string;
    appendexDocuments:string
}

interface Appellant {
    Name: string;

    email: string;
    phone: string;

}
export enum State {
    AK,
    AL,
    AR,
    AZ,
    CA,
    CO,
    CT,
    DC,
    DE,
    FL,
    GA,
    HI,
    IA,
    ID,
    IL,
    IN,
    KS,
    KY,
    LA,
    MA,
    MD,
    ME,
    MI,
    MN,
    MO,
    MS,
    MT,
    NC,
    ND,
    NE,
    NH,
    NJ,
    NM,
    NV,
    NY,
    OH,
    OK,
    OR,
    PA,
    RI,
    SC,
    SD,
    TN,
    TX,
    UT,
    VA,
    VT,
    WA,
    WI,
    WV,
    WY

}
enum County {
    Adams,
    Ashland,
    Barron,
    Bayfield,
    Brown,
    Buffalo,
    Burnett,
    Calumet,
    Chippewa,
    Clark,
    Columbia,
    Crawford,
    Dane,
    Dodge,
    Door,
    Douglas,
    Dunn,
    EauClaire,
    Florence,
    FondDuLac,
    Forest,
    Grant,
    Green,
    GreenLake,
    Iowa,
    Iron,
    Jackson,
    Jefferson,
    Juneau,
    Kenosha,
    Kewaunee,
    LaCrosse,
    Lafayette,
    Langlade,
    Lincoln,
    Manitowoc,
    Marathon,
    Marinette,
    Marquette,
    Menominee,
    Milwaukee,
    Monroe,
    Oconto,
    Oneida,
    Outagamie,
    Ozaukee,
    Pepin,
    Pierce,
    Polk,
    Portage,
    Price,
    Racine,
    Richland,
    Rock,
    Rusk,
    Sauk,
    Sawyer,
    Shawano,
    Sheboygan,
    StCroix,
    Taylor,
    Trempealeau,
    Vernon,
    Vilas,
    Walworth,
    Washburn,
    Washington,
    Waukesha,
    Waupaca,
    Waushara,
    Winnebago,
    Wood
    
}
enum Role {
    Plantiff,
    Defendent
}
interface CircuitCourtCase {
    county: County;
    caseNumber: string;
    role: Role;
    judgeFirstName: string;
    judgeLastName: string;
    opponentName: string;




}
interface Address {
    street: string;
    street2: string;
    city: string;
    state: State;
    zip: string;

}
@NgModule({
    imports: [
        BrowserModule,
        HttpModule
    ]

})
@Component({
    selector: 'dataForm',
    templateUrl: './form.component.html',
   // viewProviders: [HttpModule],
    styleUrls: ['./form.component.css']
})
export class FormComponent {
    briefInfo: BriefInfo;
    appellant: Appellant;
    address: Address;
    circuitCourtCase: CircuitCourtCase;
    ngOnInit() {
        //initialize form

        this.appellant = {
            Name: "",
            email:"",
            phone: ""

        }
        this.address= {
            street: "",
            street2:"",
            city:"",
            state: State.WI,
            zip:"",

        }
        this.circuitCourtCase = {
            county: County.Milwaukee,
            caseNumber:'',
            role: Role.Defendent,
            judgeFirstName:'',
            judgeLastName: '',
            opponentName:'',
        }
        this.briefInfo = {
            appellant: this.appellant,
            issuesPresented: "",
            oralArgumentStatement: "",
            publicationStatement: "",
            caseFactsStatement: "",
            argument: "",
            conclusion:"",
            appendexDocuments: ""

        }

    }
    constructor(private http: Http, private router: Router) { } 
    onSubmitTemplateBased(form: NgForm) {
        let params = new URLSearchParams();
        console.log(form.value);
        console.log(this.appellant);
        console.log(this.address);
        console.log(this.briefInfo);
        console.log(this.circuitCourtCase);
        var appJSON = JSON.stringify(this.appellant);
        console.log(appJSON);
        var addrJSON = JSON.stringify(this.address);
        var briefJSON = JSON.stringify(this.briefInfo); 
        var caseJSON = JSON.stringify(this.circuitCourtCase); 
        params.append("BriefInfo", briefJSON);
        params.append("Appellant", appJSON);
        params.append("Address", addrJSON);
        params.append("CircuitCourtCase", caseJSON);
        

        this.http.post('/api/POST', params).subscribe(
            data => {
                alert('ok');
            },
            error => {
                console.log(JSON.stringify(error.json()))
            }
        )
        this.router.navigateByUrl('/final');
        
        
    }
    sendData(info: BriefInfo ) {
        let params = new URLSearchParams();
        var sendobj = JSON.stringify(info);
        params.append("BriefInfo", sendobj);
        params.append("Appellant", sendobj);

        return this.http.post('/api', params).subscribe(
            data => {
                alert('ok');
            },
            error => {
                console.log(JSON.stringify(error.json()))
            }
        )


    }
    


}
