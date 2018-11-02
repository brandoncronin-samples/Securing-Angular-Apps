import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({providedIn: 'root'})
export class AdminRouteGuard implements CanActivate {
    constructor(private _authService: AuthService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this._authService.authContext &&
        this._authService.authContext.claims &&
        !!this._authService.authContext.claims.find(c => 
            c.type === 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role' &&
            c.value === 'Admin');
    }
}