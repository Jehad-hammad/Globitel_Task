
import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import decode from 'jwt-decode';
import { environment } from 'src/environments/environment';
import { AuthService } from '../services/auth.service';

@Injectable()
export class RoleGuard implements CanActivate {

  constructor(public auth: AuthService, public router: Router) { }


  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRoles = route.data.expectedRole as [];
    const token = localStorage.getItem(environment.token);
    const tokenPayload = decode(token);

    if (this.auth.isAuthenticated()) {
      var isValidRole: boolean = false;
      var userRols = (tokenPayload as any)[environment.roleClaim] as [];

      if (userRols) {
        if (Array.isArray(userRols)) {
          userRols.forEach(role => {
            if (expectedRoles.includes(role)) {
              isValidRole = true;
            }
          });
        } else {
          if (expectedRoles.includes(userRols)) {
            isValidRole = true;
          }
        }
      }

      if (!isValidRole) {
        this.router.navigate(['/unauthorized']);
        return false;
      }
      else {
        return true;
      }
    }
    else {
      this.router.navigate(['/unauthorized']);
      return false;
    }
  }
}
