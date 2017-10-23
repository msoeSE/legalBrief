import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpModule, URLSearchParams ,Http} from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';

interface BriefInfo {
    appellant: Appellant;
    issuesPresented: string;
    oralArgumentStatement: string;
    publicationStatement: string;
    caseFactsStatement : string;
    argument: string;
    appendexDocuments:string
}

interface Appellant {
    firstName: string;
    lastName: string;
    email: string;
    phone: string;

}
enum State {
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
    opponentFirstName: string;
    opponentLastName: string;



}
interface Address {
    Street: string;
    Street2: string;
    City: string;
    State: State;
    Zip: string;

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
    constructor(private http: Http) { } 
    sendData(info: BriefInfo ) {
        let params = new URLSearchParams();
        var sendobj = JSON.stringify(info);
        params.set("BriefInfo", sendobj);

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
