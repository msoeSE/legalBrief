import {Email} from "./Email";

export class RegistrationRequest {
    email: Email;
    password: string;

    constructor() {
        this.email = new Email();
        this.password = "";
    }
}