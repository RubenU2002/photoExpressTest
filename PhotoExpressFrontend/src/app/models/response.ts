export interface Response<T> {
    data?: T;
    isSuccess: boolean;
    errorMessage?: string;
}