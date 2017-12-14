export class ResetPasswordRequest {

    email: string;
    password: string;
    confirmPassword: string;
    code: string;

    constructor() {
        this.email = "";
        this.password = "";
        this.confirmPassword = "";
        this.code = "";
    }
}