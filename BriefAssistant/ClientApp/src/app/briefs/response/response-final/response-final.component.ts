import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from "@angular/forms";

import { BriefService } from '../../shared/brief.service';
import { EmailRequest } from "../../../shared/EmailRequest";

@Component({
  selector: 'response-final',
  templateUrl: './response-final.component.html'
})
export class ResponseFinalComponent {
  id: string | null;

  constructor(
    readonly router: Router,
    private readonly briefService: BriefService,
    private route: ActivatedRoute
  ) {
  }

  model = new EmailRequest();

  download() {
    this.briefService.downloadBrief(this.id).subscribe(blob => {
      let url = window.URL.createObjectURL(blob);
      window.open(url);
    });
  }

  onSubmit(form: NgForm) {
    this.briefService.emailBrief(this.id, this.model)
      .subscribe(res => {
        alert("Email Sent!");
      });
  };

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
  }

  backToForm() {
    this.router.navigate(["/briefs/response", this.id]);
  }
}
