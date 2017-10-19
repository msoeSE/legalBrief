import { Component } from '@angular/core';
import { NgForm } from '@angular/common';
import { bootstrap } from '@angular/platform/browser';
import { Http, Headers, HTTP_PROVIDERS } from '@angular/http';

@Component({
    selector: 'form',
    templateUrl: './form.component.html',
    viewProviders: [HTTP_PROVIDERS],
    styleUrls: ['./form.component.css']
})
interface BriefInfo {
    Appellant: Appellant;
    IssuesPresented: string;
    OralArgumentStatement: string;
    PublicationStatement: string;
    CaseFactsStatement : string;
    Argument: string;
    AppendexDocuments:string
}

interface Appellant {
    FirstName: string;
    LastName: string;
    Email: string;
    Phone: string;

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
    County: County;
    CaseNumber: string;
    Role: Role;
    JudgeFirstName: string;
    JudgeLastName: string;
    OpponentFirstName: string;
    OpponentLastName: string;



}
interface Address {
    public Street: string;
    public Street2: string;
    public City: string;
    public State: State;
    public Zip: string;

}

export class FormComponent {
    private controllerURL: string = "/API/formData";
    sendData(IssuesPresented: string) {
        let params = new URLSearchParams();
        params.set((""))

    }
    


}
