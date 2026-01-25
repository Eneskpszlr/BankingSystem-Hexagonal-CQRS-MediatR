import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer, CreateCustomerRequest, UpdateCustomerRequest } from '../../../core/models/customers/index';
import { CommandResponse } from '../../../core/models/api-response.model';
import { API_ENDPOINTS } from '../../../core/constants/api-endpoints';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private _http = inject(HttpClient);

  getAll(): Observable<Customer[]> {
    return this._http.get<Customer[]>(API_ENDPOINTS.CUSTOMERS.BASE);
  }

  getById(id: number): Observable<Customer> {
    return this._http.get<Customer>(API_ENDPOINTS.CUSTOMERS.GET_BY_ID(id));
  }

  create(request: CreateCustomerRequest): Observable<CommandResponse> {
    return this._http.post<CommandResponse>(API_ENDPOINTS.CUSTOMERS.BASE, request);
  }

  update(request: UpdateCustomerRequest): Observable<CommandResponse> {
    return this._http.put<CommandResponse>(API_ENDPOINTS.CUSTOMERS.BASE, request);
  }

  delete(id: number): Observable<CommandResponse> {
    return this._http.delete<CommandResponse>(API_ENDPOINTS.CUSTOMERS.GET_BY_ID(id));
  }
}