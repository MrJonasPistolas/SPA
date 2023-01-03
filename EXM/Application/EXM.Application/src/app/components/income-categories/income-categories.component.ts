import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";

import { IncomeCategoryViewer, PagedResultsResponse } from "../../models";
import { IncomeCategoriesService } from "../../services";

@Component({
  selector: 'app-income-categories',
  templateUrl: './income-categories.component.html',
  styleUrls: ['./income-categories.component.css']
})
export class IncomeCategoriesComponent implements OnInit {

  columnDefs = [{ field: "make" }, { field: "model" }, { field: "price" }];

  rowData = [
    { make: "Toyota", model: "Celica", price: 35000 },
    { make: "Ford", model: "Mondeo", price: 32000 },
    { make: "Porsche", model: "Boxter", price: 72000 }
  ];

  pageNumber = 0;
  pageSize = 0;
  total = 0;
  rows = new Array<IncomeCategoryViewer>();

  // Constructor
  constructor(private http: HttpClient, private incomeCategoriesService: IncomeCategoriesService) {
    this.pageNumber = 1;
    this.pageSize = 2;
  }

  ngOnInit(): void {
    
  }

  /**
   * Populate the table with new data based on the page number
   * @param page The page to select
   */
  setPage(pageInfo: any) {
    //this.pageNumber = pageInfo.offset;
    //this.incomeCategoriesService.getAll(this.pageNumber, this.pageSize).subscribe((response: PagedResultsResponse<IncomeCategoryViewer>) => {
    //  if (response.succeeded) {
    //    this.rows = response.data;
    //    this.total = response.totalCount;
    //  }
    //});
  }
}
