export class ConfirmationRequest {
  code: string;
  email: string;

  constructor() {
    this.code = "";
    this.email = "";
  }
}
