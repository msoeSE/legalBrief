import { Component } from '@angular/core';

import { AccountService } from '../account.service';

@Component({
  selector: 'appHeader',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  constructor(public accountService: AccountService) { }

}
