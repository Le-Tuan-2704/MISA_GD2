export interface AppServerResponse<T> {
    successState: boolean,
    data: T,
    userMsg: string,
    devMsg: string
    errorCode: string
}