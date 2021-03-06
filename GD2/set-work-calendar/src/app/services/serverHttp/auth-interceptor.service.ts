import { HttpEventType, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { exhaustMap, take, tap } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService {

  private baseUrl = "https://localhost:44373/api"

  constructor(private authService: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    return this.authService.user
      .pipe(
        take(1),
        exhaustMap(user => {
          //Clone request 
          let modifiedReq = req.clone(
            {
              //Thêm auth vào header
              headers: req.headers.append(
                'Authorization',
                user?.accessToken
                  ? `Bearer ${user.accessToken}`
                  : ""
              ),
              //Thay url 
              url: `${this.baseUrl}${req.url}`
            });
          return next.handle(modifiedReq).pipe(
            tap(event => {
              //Response
              if (event.type === HttpEventType.Response) {
                //Nếu unauthorized thì logout
                if (event.status === 401) {
                  this.authService.logout();
                }
              }
            })
          );
        })
      );
  }
}
