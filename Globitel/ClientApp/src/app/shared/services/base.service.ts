import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  }

  constructor(private http: HttpClient) {

  }

  public getAllItems(controllerName: string) {
    return this.http.get(/api/+ controllerName + 'GetAllItems');
  }

  public getList(controllerName: string, searchModel = null): Observable<any[]> {
    return this.http.post<any[]>(/api/+ controllerName + 'GetList', JSON.stringify(searchModel), this.httpOptions);
  }

  public getById(controllerName: string, id:number) {
    return this.http.get('/api/' + controllerName + 'GetById/' + id );
  }

  public postItem(controllerName: string, postObject ) {
    return this.http.post(/api/+ controllerName + 'PostItem', JSON.stringify(postObject), this.httpOptions);
  }

  public PostRange(controllerName: string, postobject) {
    return this.http.post(/api/ + controllerName + 'PostRange', JSON.stringify(postobject), this.httpOptions);
  }

  public editItem(controllerName: string, editObject) {
    return this.http.put(/api/ + controllerName + 'EditItem', JSON.stringify(editObject), this.httpOptions);
  }

  public editRange(controllerName: string, editObject) {
    return this.http.put(/api/ + controllerName + 'EditRange', JSON.stringify(editObject), this.httpOptions);
  }

  public removeItemById(controllerName: string, id) {
    return this.http.delete(/api/ + controllerName + 'RemoveItemById/' + id);
  }

  public removeItem(controllerName: string, postobject) {
    return this.http.post(/api/+ controllerName + 'RemoveItem', JSON.stringify(postobject), this.httpOptions);
  }

  public removeRange(controllerName: string, postobject) {
    return this.http.post(/api/ + controllerName + 'RemoveRange', JSON.stringify(postobject), this.httpOptions);
  }
  

}
