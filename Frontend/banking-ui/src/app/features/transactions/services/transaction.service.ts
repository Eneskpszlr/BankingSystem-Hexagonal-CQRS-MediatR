import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transaction, TransactionRequest, TransferRequest } from '../../../core/models/transactions/index';
import { ApiResponse } from '../../../core/models/api-response.model';
import { API_ENDPOINTS } from '../../../core/constants/api-endpoints';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  
  private _http = inject(HttpClient);

  getByAccountId(accountId: number): Observable<ApiResponse<Transaction[]>> {
    const params = new HttpParams().set('accountId', accountId);

    return this._http.get<ApiResponse<Transaction[]>>(API_ENDPOINTS.TRANSACTIONS.BASE, { params });
  }

  deposit(request: TransactionRequest): Observable<ApiResponse<number>> {
    return this._http.post<ApiResponse<number>>(API_ENDPOINTS.TRANSACTIONS.DEPOSIT, request);
  }

  withdraw(request: TransactionRequest): Observable<ApiResponse<number>> {
    return this._http.post<ApiResponse<number>>(API_ENDPOINTS.TRANSACTIONS.WITHDRAW, request);
  }

  transfer(request: TransferRequest): Observable<ApiResponse<number>> {
    return this._http.post<ApiResponse<number>>(API_ENDPOINTS.TRANSACTIONS.TRANSFER, request);
  }
}