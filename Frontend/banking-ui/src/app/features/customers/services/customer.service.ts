import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer, CreateCustomerRequest, UpdateCustomerRequest } from '../../../core/models/customers/index';
import { ApiResponse } from '../../../core/models/api-response.model';
import { API_ENDPOINTS } from '../../../core/constants/api-endpoints';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  
  private _http = inject(HttpClient);

  getAll(): Observable<ApiResponse<Customer[]>> {
    return this._http.get<ApiResponse<Customer[]>>(API_ENDPOINTS.CUSTOMERS.BASE);
  }

  getById(id: number): Observable<ApiResponse<Customer>> {
    return this._http.get<ApiResponse<Customer>>(API_ENDPOINTS.CUSTOMERS.GET_BY_ID(id));
  }

  create(request: CreateCustomerRequest): Observable<ApiResponse<number>> {
    return this._http.post<ApiResponse<number>>(API_ENDPOINTS.CUSTOMERS.BASE, request);
  }

  update(request: UpdateCustomerRequest): Observable<ApiResponse<null>> {
    return this._http.put<ApiResponse<null>>(API_ENDPOINTS.CUSTOMERS.BASE, request);
  }

  delete(id: number): Observable<ApiResponse<null>> {
    return this._http.delete<ApiResponse<null>>(API_ENDPOINTS.CUSTOMERS.GET_BY_ID(id));
  }
}