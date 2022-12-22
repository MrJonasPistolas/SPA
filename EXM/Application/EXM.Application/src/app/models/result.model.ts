export interface Result<T> {
  data: T;
  messages: Array<string>;
  succeeded: boolean;
}
