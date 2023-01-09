import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { environment } from "../../environments/environment";
import { QueryHelper } from "../helpers";
import { IQueryString } from "../interfaces";
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

  getAll(pageNumber: number, pageSize: number, searchString: string): Observable<PagedResultsResponse<IncomeCategoryViewer>> {
    let url = `${this.apiUrl}/api/v1/IncomeCategories?`;

    let queryStrings: Array<IQueryString> = [];

    queryStrings.push(
      {
        key: 'pageNumber',
        value: pageNumber.toString()
      },
      {
        key: 'pageSize',
        value: pageSize.toString()
      }
    );

    if (searchString) {
      queryStrings.push({
        key: 'searchString',
        value: searchString
      });
    }

    url = QueryHelper.AddQueryStrings(url, queryStrings);

    return this.http.get<PagedResultsResponse<IncomeCategoryViewer>>(url);
  }
}
