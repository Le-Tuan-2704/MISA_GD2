import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BaseServiceService {
  public viewApp = new BehaviorSubject<string>("VIEW");
  public viewCalendar = new BehaviorSubject<string>("MONTH");
  constructor() { }

  public setViewApp(view: string) {
    console.log("view", view);
    this.viewApp.next(view);
  }
  public setViewCalendar(view: string) {
    console.log("calendar", view);
    this.viewCalendar.next(view);
  }
}
