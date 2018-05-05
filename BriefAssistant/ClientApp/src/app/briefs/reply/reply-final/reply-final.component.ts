import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from "@angular/forms";
import { saveAs } from 'file-saver';

import { BriefService } from '../../shared/brief.service';
import { EmailRequest } from '../../../shared/EmailRequest';

@Component({
    selector: 'replyFinal',
    templateUrl: './reply-final.component.html'
})
export class ReplyFinalComponent {
    private id: string | null;

    constructor(
      readonly router: Router,
      private readonly briefService: BriefService,
      private route: ActivatedRoute
    ) { }

    model = new EmailRequest();

  download() {
    this.briefService.downloadBrief(this.id).subscribe(file => {
      saveAs(file);
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
      this.router.navigate(["/briefs/reply", this.id]);
    }
}
