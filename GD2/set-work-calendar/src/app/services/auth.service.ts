import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { AuthResponseData } from '../models/auth-response-data.model';
import { AppServerResponse } from '../models/service-response.model';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  //user hiện tại
  user = new BehaviorSubject<User>(null);

  private tokenExperationTimer: any;

  private baseRoute = '/Auth';

  constructor(private http: HttpClient, private router: Router) { }

  /**
     * Đăng nhập
     * @param username 
     * @param password 
     * @returns 
     */
  login(username: string, password: string) {
    return this.http
      .post<AppServerResponse<AuthResponseData>>(
        this.baseRoute + '/login',
        {
          username: username,
          password: password,
        }
      )
      .pipe(
        catchError(errorRes => {
          return this.handleError(errorRes);
        }),
        tap(resData => {
          if (resData.success) {
            this.handleAuthentication(
              resData.data.user.email,
              resData.data.user.userId,
              resData.data.user.username,
              resData.data.user.employeeId,
              resData.data.user.avatar,
              resData.data.accessToken,
            );
          }
        }),

      )
  }

  /**
     * Handle lỗi từ response
     * @param errorRes 
     * @returns 
     */
  private handleError(errorRes: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    if (!errorRes.error) {
      return throwError(errorMessage);
    }
    errorMessage = errorRes.error.message;
    return throwError(errorMessage);
  }

  /**
     * Hàm handle xác thực
     * @param email 
     * @param userId 
     * @param username 
     * @param avatar 
     * @param accessToken 
     * @param refreshToken 
     */
  private handleAuthentication(
    email: string,
    userId: string,
    username: string,
    employeeId: string,
    avatar: string,
    accessToken: string,
  ) {
    let decodedToken = jwtDecode(accessToken);

    let accessExpiresTime = +decodedToken['exp'] * 1000;


    const accessTokenExpDate = new Date(accessExpiresTime)

    //Tạo user mới dựa trên thông tin ở trên
    const user = new User(userId, username, email, employeeId, avatar, accessToken, accessTokenExpDate);

    //Set người dùng mới
    this.user.next(user);

    //Lưu vào localStorage
    localStorage.setItem('userData', JSON.stringify(user));
  }
}
