import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Action, Role, RoleAction, RoleActionUpdateRequest } from '../constants';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private baseUrl = 'api/admin/';

  constructor(private http: HttpClient) { }

  getRoles(): Observable<Role[]> {
    const url = `${this.baseUrl}roles`;
    return this.http.get<Role[]>(url);
  }

  getActions(): Observable<Action[]> {
    const url = `${this.baseUrl}actions`;
    return this.http.get<Action[]>(url);
  }

  getRoleActions(roleId: number): Observable<RoleAction[]> {
    const url = `${this.baseUrl}roleActions`;
    const params = new HttpParams().set('roleId', roleId.toString());
    return this.http.get<RoleAction[]>(url, { params });
  }

  updateRoleAction(request: RoleActionUpdateRequest): Observable<void> {
    const url = `${this.baseUrl}updateRoleAction`;
    return this.http.post<void>(url, request);
  }
  
}
