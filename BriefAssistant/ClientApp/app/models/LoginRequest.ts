export class LoginRequest {
    email: string;
    password: string;
    rememberMe: boolean;

    constructor() {
        this.email = "";
        this.password = "";
        this.rememberMe = false;
    }
}