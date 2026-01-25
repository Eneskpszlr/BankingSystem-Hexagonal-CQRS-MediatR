import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Account, CreateAccountRequest, UpdateAccountRequest } from '../../../core/models/accounts/index';
import { CommandResponse } from '../../../core/models/api-response.model'; 
import { API_ENDPOINTS } from '../../../core/constants/api-endpoints';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private _http = inject(HttpClient);

  getAll(): Observable<Account[]> {
    return this._http.get<Account[]>(API_ENDPOINTS.ACCOUNTS.BASE);
  }

  getById(id: number): Observable<Account> {
      return this._http.get<Account>(API_ENDPOINTS.ACCOUNTS.GET_BY_ID(id));
  }

  getByCustomerId(customerId: number): Observable<Account[]> {
    return this._http.get<Account[]>(API_ENDPOINTS.ACCOUNTS.GET_BY_CUSTOMER(customerId));
  }

  create(request: CreateAccountRequest): Observable<CommandResponse> {
    return this._http.post<CommandResponse>(API_ENDPOINTS.ACCOUNTS.BASE, request);
  }

  update(request: UpdateAccountRequest): Observable<CommandResponse> {
    return this._http.put<CommandResponse>(API_ENDPOINTS.ACCOUNTS.BASE, request);
  }

  delete(id: number): Observable<CommandResponse> {
    return this._http.delete<CommandResponse>(API_ENDPOINTS.ACCOUNTS.GET_BY_ID(id));
  }
}