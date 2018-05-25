import { Component } from '@angular/core';

import { faSignInAlt, faSignOutAlt } from '@fortawesome/free-solid-svg-icons';

import { AuthService } from '../auth.service';

@Component({
  selector: 'appHeader',
  templateUrl: './header.component.html'
})
export class HeaderComponent {
  // Expose Icons to template
  faSignIn = faSignInAlt;
  faSignOut = faSignOutAlt;

  isCollapsed = true;

  constructor(public authService: AuthService) { }

  toggle() {
    this.isCollapsed = !this.isCollapsed;
  }
}
