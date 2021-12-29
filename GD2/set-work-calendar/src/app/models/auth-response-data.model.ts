export interface AuthResponseData {
    user: {
        idUser: string,
        userName: string,
        role: number,
        avatar: string
    };
    accessToken: string,
    refreshToken: string
}