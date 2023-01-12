export interface PagedResultsResponse<T> {
  data: Array<T>;
  currentPage: number;
  totalPages: number;
  totalCount: number;
  pageSize: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  messages: string[];
  succeeded: boolean;
}
