import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Branch, CreateBranchRequest, UpdateBranchRequest } from '../../../core/models/branches/index';
import { CommandResponse } from '../../../core/models/api-response.model';
import { API_ENDPOINTS } from '../../../core/constants/api-endpoints';

@Injectable({
  providedIn: 'root'
})
export class BranchService {
  private _http = inject(HttpClient);

  getAll(): Observable<Branch[]> {
    return this._http.get<Branch[]>(API_ENDPOINTS.BRANCHES.BASE);
  }

  getById(id: number): Observable<Branch> {
    return this._http.get<Branch>(API_ENDPOINTS.BRANCHES.GET_BY_ID(id));
  }
  
  create(request: CreateBranchRequest): Observable<CommandResponse> {
    return this._http.post<CommandResponse>(API_ENDPOINTS.BRANCHES.BASE, request);
  }

  update(request: UpdateBranchRequest): Observable<CommandResponse> {
    return this._http.put<CommandResponse>(API_ENDPOINTS.BRANCHES.BASE, request);
  }

  delete(id: number): Observable<CommandResponse> {
    return this._http.delete<CommandResponse>(API_ENDPOINTS.BRANCHES.GET_BY_ID(id));
  }
}