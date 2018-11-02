import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserManager, User, WebStorageStateStore, Log } from 'oidc-client';
import { Constants } from '../constants';
import { AuthContext } from '../model/auth-context';
import { Utils } from './utils';

@Injectable({providedIn: 'root'})
export class AuthService {
    private _userManager: UserManager;
    private _user: User;
    authContext: AuthContext;

    constructor(private httpClient: HttpClient) {
        Log.logger = console;
        var config = {
            authority: Constants.stsAuthority,
            client_id: 'spa-client',
            redirect_uri: `${Constants.clientRoot}assets/oidc-login-redirect.html`,
            scope: 'openid projects-api profile',
            response_type: 'id_token token',
            post_logout_redirect_uri: `${Constants.clientRoot}?postLogout=true`,
            userStore: new WebStorageStateStore({ store: window.localStorage }),
            automaticSilentRenew: true,
            silent_redirect_uri: `${Constants.clientRoot}assets/silent-redirect.html`
        };
        this._userManager = new UserManager(config);
        this._userManager.getUser().then(user => {
            if (user && !user.expired) {
                this._user = user;
                this.loadSecurityContext();
            }
        });
        this._userManager.events.addUserLoaded(() => {
            this._userManager.getUser().then(user => {
                this._user = user;
                this.loadSecurityContext();
            });
        });
     }

     login(): Promise<any> {
         return this._userManager.signinRedirect();
     }

     logout(): Promise<any> {
         return this._userManager.signoutRedirect();
     }

     isLoggedIn(): boolean {
         return this._user && this._user.access_token && !this._user.expired;
     }

     getAccessToken(): string {
         return this._user ? this._user.access_token : '';
     }

     signoutRedirectCallback(): Promise<any> {
         return this._userManager.signoutRedirectCallback();
     }

     loadSecurityContext() {
         this.httpClient.get<AuthContext>(`${Constants.apiRoot}Account/AuthContext`)
         .subscribe(context => {
             this.authContext = context;
         }, error => console.error(Utils.formatError(error)));
     }
    
}