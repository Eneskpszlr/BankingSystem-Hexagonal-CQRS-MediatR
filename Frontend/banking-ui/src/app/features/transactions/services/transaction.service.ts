import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transaction, TransactionRequest, TransferRequest } from '../../../core/models/transactions/index';
import { ApiResponse } from '../../../core/models/api-response.model';
import { API_ENDPOINTS } from '../../../core/constants/api-endpoints';

// Filtreleme için tip tanımı
export interface TransactionFilter {
  accountId?: number;
  startDate?: string; // YYYY-MM-DD
  endDate?: string;
}

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  
  private _http = inject(HttpClient);

  getAll(filter: TransactionFilter = {}): Observable<ApiResponse<Transaction[]>> {
    let params = new HttpParams();

    // 1. Hesap ID var mı?
    if (filter.accountId) {
      params = params.set('accountId', filter.accountId);
    }

    // 2. Başlangıç Tarihi var mı?
    if (filter.startDate) {
      params = params.set('startDate', filter.startDate);
    }

    // 3. Bitiş Tarihi var mı?
    if (filter.endDate) {
      params = params.set('endDate', filter.endDate);
    }

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