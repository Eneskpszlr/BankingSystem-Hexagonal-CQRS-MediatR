import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Account, CreateAccountRequest, UpdateAccountRequest } from '../../../core/models/accounts/index';
import { ApiResponse } from '../../../core/models/api-response.model';
import { API_ENDPOINTS } from '../../../core/constants/api-endpoints';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  
  private _http = inject(HttpClient);

  getAll(): Observable<ApiResponse<Account[]>> {
    return this._http.get<ApiResponse<Account[]>>(API_ENDPOINTS.ACCOUNTS.BASE);
  }

  getById(id: number): Observable<ApiResponse<Account>> {
    return this._http.get<ApiResponse<Account>>(API_ENDPOINTS.ACCOUNTS.GET_BY_ID(id));
  }

  getByCustomerId(customerId: number): Observable<ApiResponse<Account[]>> {
    return this._http.get<ApiResponse<Account[]>>(API_ENDPOINTS.ACCOUNTS.GET_BY_CUSTOMER(customerId));
  }

  create(request: CreateAccountRequest): Observable<ApiResponse<number>> {
    return this._http.post<ApiResponse<number>>(API_ENDPOINTS.ACCOUNTS.BASE, request);
  }

  update(request: UpdateAccountRequest): Observable<ApiResponse<null>> {
    return this._http.put<ApiResponse<null>>(API_ENDPOINTS.ACCOUNTS.BASE, request);
  }

  delete(id: number): Observable<ApiResponse<null>> {
    return this._http.delete<ApiResponse<null>>(API_ENDPOINTS.ACCOUNTS.GET_BY_ID(id));
  }
}