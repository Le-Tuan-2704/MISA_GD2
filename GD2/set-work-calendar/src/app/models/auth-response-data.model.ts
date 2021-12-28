export interface AuthResponseData {
    user: {
        email: string,
        userId: string,
        username: string,
        role: number,
        avatar: string
    };
    accessToken: string,
    refreshToken: string
}