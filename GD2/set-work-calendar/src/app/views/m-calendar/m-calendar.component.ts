import { Component, OnInit } from '@angular/core';

import { CalendarEvent } from 'src/app/models/event.model';
import { BaseServiceService } from 'src/app/services/baseService/base-service.service';
import { CalendarService } from 'src/app/services/serverHttp/calendar.service';


@Component({
  selector: 'app-m-calendar',
  templateUrl: './m-calendar.component.html',
  styleUrls: ['./m-calendar.component.css']
})
export class MCalendarComponent implements OnInit {
  public viewCalendar = "MONTH";

  //Hiện form thêm mới
  isAddFormShow = false;
  //Hiện thông tin chi tiết
  isEventDetailsShow = false;

  selectingEventId: string = "";

  eventCardLoading = false;

  selectingEvent: CalendarEvent;
  // {
  //   if (this.selectingEventId) {
  //     console.log(this.calendarService.findEventById(this.selectingEventId));

  //     return this.calendarService.findEventById(this.selectingEventId);
  //   }
  //   return null;
  // }

  constructor(
    private baseService: BaseServiceService,
    private calendarService: CalendarService
  ) {

  }

  ngOnInit(

  ): void {
    this.baseService.viewCalendar.subscribe((view) => {
      this.viewCalendar = view;
    });
  }

  /**
     * Handle sự kiện từ router view
     * @param elementRef 
     */
  onActivate(elementRef) {
    // console.log(elementRef.event._def.extendedProps.eventcalendarId);
    this.showEventDetails(elementRef.event._def.extendedProps.eventcalendarId);
  }

  /**
     * Hiển thị thông tin chi tiết sự kiện
     * @param id 
     */
  showEventDetails(id: string) {
    this.selectingEventId = id;
    this.selectingEvent = this.calendarService.events.find((el) => {
      return el.eventcalenderId == id;
    });
    this.isEventDetailsShow = true;
  }

  /**
     * Xóa sự kiện
     * @param id 
     */
  deleteEvent(id: string) {

  }

  /**
   * Hoàn thành sự kiện
   * @param id 
   */
  completeEvent(id: string) {
  }
}
