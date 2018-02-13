export class RegistrationRequest {
    email: string;
    password: string;
    confirmPassword: string;

    constructor() {
        this.email = "";
        this.password = "";
        this.confirmPassword = "";
    }
}
