import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AccountService } from '../shared/account.service';

@Component({
  selector: 'confirmation',
  templateUrl: "./confirmation.component.html",
})
export class ConfirmationComponent implements OnInit {
  confirmationSuccessful = false;
  confirmationFailed = false;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly accountService: AccountService
  ) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
        this.accountService.confirmEmail(params['userId'], params['code'])
          .subscribe(
            res => this.confirmationSuccessful = true,
            err => this.confirmationFailed = true
        );
    });
  }
}
