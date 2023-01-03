import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { environment } from "../../environments/environment";
import {
  IncomeCategoryViewer,
  PagedResultsResponse
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

  getAll(pageNumber: number, pageSize: number): Observable<PagedResultsResponse<IncomeCategoryViewer>> {
    return this.http.get<PagedResultsResponse<IncomeCategoryViewer>>(`${this.apiUrl}/api/v1/IncomeCategories?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }
}
