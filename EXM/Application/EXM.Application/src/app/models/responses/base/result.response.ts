export interface ResultResponse<T> {
  data: T;
  messages: string[];
  succeeded: boolean;
}
