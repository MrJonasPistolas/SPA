import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { environment } from "../../environments/environment";
import { QueryHelper, ToolsHelper } from "../helpers";
import { IQueryString } from "../interfaces";
import {
  IncomeCategoryRequest,
  IncomeCategoryViewer,
  PagedResultsResponse,
  ResultResponse
} from "../models";

@Injectable({
  providedIn: 'root'
})
export class IncomeCategoriesService {
  // Properties
  private readonly apiUrl = `${environment.serverUrl}`;

  // Constructor
  constructor(
    private http: HttpClient
  ) { }

  getPaged(params: any): Observable<any> {
    let url = `${this.apiUrl}/api/v1/IncomeCategories/Paged`;
    return this.http.post(url, params);
  }

  getById(id: string): Observable<ResultResponse<IncomeCategoryViewer>> {
    let url = `${this.apiUrl}/api/v1/IncomeCategories/${id}`;

    return this.http.get<ResultResponse<IncomeCategoryViewer>>(url);
  }

  upsert(request: IncomeCategoryRequest): Observable<ResultResponse<IncomeCategoryViewer>> {
    let url = `${this.apiUrl}/api/v1/IncomeCategories`;

    if (request.id == 0)
      return this.http.post<ResultResponse<IncomeCategoryViewer>>(url, request);
    else
      return this.http.put<ResultResponse<IncomeCategoryViewer>>(url, request);
  }
}
