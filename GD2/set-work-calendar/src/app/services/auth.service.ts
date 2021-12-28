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
      .get<AppServerResponse<AuthResponseData>>(
        this.baseRoute + `/login?userName=${username}&password=${password}`,
      )
      .pipe(
        catchError(errorRes => {
          return this.handleError(errorRes);
        }),
        tap(resData => {
          console.log(resData);

          if (resData.successState) {
            this.handleAuthentication(
              resData.data.user.userId,
              resData.data.user.username,
              resData.data.user.avatar,
              resData.data.user.role,
              resData.data.accessToken,
            );
          }
        }),

      )
  }

  /**
    * Tự động đăng nhập lại
    */
  autoLogin() {
    //Lấy userData từ local storage
    const userData = JSON.parse(localStorage.getItem('userData'));

    if (!userData) {
      return;
    }

    //Cast sang dạng User
    const loadedUser = new User(
      userData['userId'],
      userData['username'],
      userData['avatar'],
      userData['role'],
      userData['_accessToken'],
      new Date(userData['_accessTokenExpDate'])
    )

    //Nếu token không hợp lệ thì thử refresh lại token
    if (new Date().getTime() > loadedUser.accessTokenExpDate.getTime()
      || !loadedUser.accessTokenExpDate
      || !loadedUser.accessToken
    ) {
      this.logout();
    } else {
      if (loadedUser.accessToken) {
        //gán user mới tạo
        this.user.next(loadedUser);
      }
    }
  }

  /**
     * Thoát đăng nhập
     */
  logout() {

    //Set lại người dùng về null    
    this.user.next(null);

    //Xóa khỏi localStorage
    localStorage.removeItem('userData');

    //Clear timer
    if (this.tokenExperationTimer) {
      clearTimeout(this.tokenExperationTimer);
    }

    this.tokenExperationTimer = null

    //Quay về trang đăng nhập
    this.router.navigate(['auth']);
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
    userId: string,
    username: string,
    avatar: string,
    role: number,
    accessToken: string,
  ) {
    let decodedToken = jwtDecode(accessToken);
    let accessExpiresTime = +decodedToken['exp'] * 1000;
    const accessTokenExpDate = new Date(accessExpiresTime)
    //Tạo user mới dựa trên thông tin ở trên
    const user = new User(userId, username, avatar, role, accessToken, accessTokenExpDate);
    //Set người dùng mới
    this.user.next(user);
    //Lưu vào localStorage
    localStorage.setItem('userData', JSON.stringify(user));
  }
}
