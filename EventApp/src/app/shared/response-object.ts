export interface IResponseObject<T> {
  data: T | undefined;
  hasError: boolean;
  error: any | undefined;
}
