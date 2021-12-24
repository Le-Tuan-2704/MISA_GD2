export interface AuthResponseData {
    user: {
        email: string,
        userId: string,
        username: string,
        employeeId: string,
        avatar: string
    };
    accessToken: string,
    refreshToken: string
}