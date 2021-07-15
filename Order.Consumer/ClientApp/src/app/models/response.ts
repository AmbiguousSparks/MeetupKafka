export interface Response<T> {
  statusCode: number;
  error: boolean;
  errors: string[];
  result: T;
}
