import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Branch, CreateBranchRequest, UpdateBranchRequest } from '../../../core/models/branches/index';
import { ApiResponse } from '../../../core/models/api-response.model';
import { API_ENDPOINTS } from '../../../core/constants/api-endpoints';

@Injectable({
  providedIn: 'root'
})
export class BranchService {
  
  private _http = inject(HttpClient);

  getAll(): Observable<ApiResponse<Branch[]>> {
    return this._http.get<ApiResponse<Branch[]>>(API_ENDPOINTS.BRANCHES.BASE);
  }

  getById(id: number): Observable<ApiResponse<Branch>> {
    return this._http.get<ApiResponse<Branch>>(API_ENDPOINTS.BRANCHES.GET_BY_ID(id));
  }
  
  create(request: CreateBranchRequest): Observable<ApiResponse<number>> {
    return this._http.post<ApiResponse<number>>(API_ENDPOINTS.BRANCHES.BASE, request);
  }

  update(request: UpdateBranchRequest): Observable<ApiResponse<null>> {
    return this._http.put<ApiResponse<null>>(API_ENDPOINTS.BRANCHES.BASE, request);
  }

  delete(id: number): Observable<ApiResponse<null>> {
    return this._http.delete<ApiResponse<null>>(API_ENDPOINTS.BRANCHES.GET_BY_ID(id));
  }
}