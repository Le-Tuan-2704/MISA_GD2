import jwtDecode from "jwt-decode";

/**
 * Model cho người dùng
 */
export class User {
    constructor(
        public id: string,
        public username: string,
        public avatar: string,
        public role: number,

        private _accessToken: string,
        private _accessTokenExpDate: Date,
    ) { }

    get accessToken() {
        //kiểm tra hạn
        if (!this._accessTokenExpDate || new Date() > this._accessTokenExpDate) {
            return null;
        }
        return this._accessToken;
    }

    get accessTokenExpDate() {
        return this._accessTokenExpDate;
    }
}