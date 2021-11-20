import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
providedIn :'root'
})
export class UsersService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  }

    constructor(private http:HttpClient) {

  }

  getLoggedInUser() {
    return this.http.get('/api/User/GetUserProfile');
  }

  updateProfile(profileObject) {
    return this.http.post('/api/User/UpdateUserProfile', JSON.stringify(profileObject), this.httpOptions);
  }

  getListOfUsers(searchObject) {
    return this.http.post('/api/User/GetUsersList', JSON.stringify(searchObject), this.httpOptions);
  }
  getChartData() {
    return this.http.get('/api/User/GetChartData');
  }
  getUserInfo(userId:number) {
    return this.http.get('/api/User/GetUserInfo/'+userId);
  }
  updateUserInfo(userObject) {
    return this.http.post('/api/User/UpdateUserInfo', JSON.stringify(userObject), this.httpOptions);
  }
}
