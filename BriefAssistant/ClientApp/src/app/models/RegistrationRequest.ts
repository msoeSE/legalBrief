import { UserType } from "./UserType";

export class RegistrationRequest {
    email: string;
    password: string;
    userType: UserType;
    confirmPassword: string;

    constructor() {
        this.email = "";
        this.password = "";
        this.confirmPassword = "";
        this.userType = UserType.ProSe;
    }
}
