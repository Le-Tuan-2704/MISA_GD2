import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
import { CalendarEvent } from 'src/app/models/event.model';

@Injectable({
  providedIn: 'root'
})

export class CalendarService {

  public eventId: string;
  public events: CalendarEvent[];

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      // Authorization: 'my-auth-token'
    })
  };

  constructor(private httpClient: HttpClient) { }

  /**
   *  lấy tất cả even
   * @returns 
   */
  public getCalendar(): Observable<any> {
    const url = `/Event/All`;
    return this.httpClient
      .get(url, this.httpOptions)
      .pipe(catchError(this.handleError));
  }

  /**
   *  thêm event
   * @param body 
   * @returns 
   */
  public addCalendar(body: {}) {
    const url = `/Event`;
    return this.httpClient
      .post<any>(url, body, this.httpOptions)
      .pipe(catchError(this.handleError));
  }

  /**
     * Tìm event trong sources theo id
     * @param id 
     * @returns 
     */
  public findEventById(id: string): CalendarEvent {

    if (this.events == [] || id == "") {
      return null;
    }
    //Tìm ở trong list đã được duyệt
    let res = this.events.find((el) => {
      console.log(el);

      return el.eventcalenderId == id;
    })
    return res;
  }


  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(
      'Something bad happened; please try again later.');
  }
}
