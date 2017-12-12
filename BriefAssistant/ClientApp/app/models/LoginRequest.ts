import { Email } from "./Email";

export class LoginRequest {
    email: Email;
    password: string;
    rememberMe: boolean;

    constructor() {
        this.email = new Email();
        this.password = "";
        this.rememberMe = false;
    }
}