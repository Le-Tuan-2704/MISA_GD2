export interface AppServerResponse<T> {
    success: boolean,
    data: T,
    userMsg: string,
    devMsg: string
    errorCode: string
}