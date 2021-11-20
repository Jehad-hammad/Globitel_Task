import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ExcelService {
    constructor(private http:HttpClient) {

  }

  generateActiveEmployeesExcel() {
    return this.http.get('/api/Excel/GenerateActiveEmployeesReport', {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8'
      }), responseType: 'blob'
    });
  }

}
