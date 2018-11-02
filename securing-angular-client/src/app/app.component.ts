import { Component, OnInit } from '@angular/core';
import { AccountService } from './core/account.service';
import { UserProfile } from './model/user-profile';
import { MatDialog } from '@angular/material';
import { Utils } from './core/utils';
import { AuthService } from './core/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: []
})
export class AppComponent implements OnInit {
  userProfile: UserProfile;
  firstLogin = false;
  constructor(
    private _acctService: AccountService,
    public dialog: MatDialog,
    private _authService: AuthService,
    private _router: Router
  ) {}

  ngOnInit() {
    if (window.location.href.indexOf('?postLogout=true') > 0) {
      this._authService.signoutRedirectCallback().then(() => {
        let url: string = this._router.url.substring(
          0,
          this._router.url.indexOf('?')
        );
        this._router.navigateByUrl(url);
      });
    }
  }

  login() {
    this._authService.login();
  }

  logout() {
    this._authService.logout();
  }
  
  isLoggedIn() {
    return this._authService.isLoggedIn();
  }

  isAdmin() {
    return this._authService.authContext &&
      this._authService.authContext.claims &&
      (this._authService.authContext.claims.find(c => 
        c.type === 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role' &&
        c.value === 'Admin'));
  }
}