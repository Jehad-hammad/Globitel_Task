import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { JwtHelperService } from "@auth0/angular-jwt";
import { environment } from "../../../environments/environment";
import { loginDTO, RegisterDTO } from "../models/models";
import decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  }

  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private router: Router) { }


  login(loginObj: loginDTO) {
    return this.http.post(
      '/api/Auth/Login',
      JSON.stringify(loginObj),
      this.httpOptions)
  }

  register(RegisterObj: RegisterDTO) {
    return this.http.post(
      '/api/Auth/Register',
      JSON.stringify(RegisterObj),
      this.httpOptions)
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem(environment.token);
    if (token) {
      return !this.jwtHelper.isTokenExpired(token);
    }
    return false;
  }

  public setToken(token: string) {
    localStorage.setItem(environment.token, token);
  }

  public logout() {
    localStorage.removeItem(environment.token);
    this.router.navigate(['/auth/login'])
  }

  get isUserLoggedIn() {
    const token = localStorage.getItem(environment.token);
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    return false;
  }

  get isAdmin() {
    const expectedRoles = ['Admin'];
    const token = localStorage.getItem(environment.token);
    const tokenPayload = decode(token);
    var userRoles = (tokenPayload as any)[environment.roleClaim] as string[];
    if (Array.isArray(userRoles)) {
      userRoles.forEach(role => {
        if (expectedRoles.includes(role)) {
          return  true;
        }
      });
    } else {
      if (expectedRoles.includes(userRoles)) {
        return true;
      }
      
    }
  }
}
