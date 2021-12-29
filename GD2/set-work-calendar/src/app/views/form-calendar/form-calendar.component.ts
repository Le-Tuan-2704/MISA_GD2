import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { BaseServiceService } from 'src/app/services/baseService/base-service.service';
import { NotificationService } from 'src/app/services/baseService/notification.service';

import { CalendarService } from 'src/app/services/serverHttp/calendar.service';

@Component({
  selector: 'app-form-calendar',
  templateUrl: './form-calendar.component.html',
  styleUrls: ['./form-calendar.component.css']
})
export class FormCalendarComponent implements OnInit {

  //Đang loading
  isLoading = false;

  error: string = "";


  constructor(
    private calendarHttp: CalendarService,
    private notificationService: NotificationService,
    private baseService: BaseServiceService
  ) { }

  ngOnInit(): void {
  }


  public onSubmit(form: NgForm) {
    //Kiểm tra form valid
    if (!form.valid) {
      return;
    }

    //Lấy giá trị từ form
    const title = form.value.title;
    const content = form.value.content;
    const startDate = form.value.startDate;
    const startTime = form.value.startTime;
    const endDate = form.value.endDate;
    const endTime = form.value.endTime;

    //gộp ngày và giờ
    let startDateTime = new Date(startDate);
    startDateTime.setUTCHours(startTime.split(':')[0]);
    startDateTime.setUTCMinutes(startTime.split(':')[1]);

    let endDateTime = new Date(endDate);
    endDateTime.setUTCHours(endTime.split(':')[0]);
    endDateTime.setUTCMinutes(startTime.split(':')[1]);

    this.isLoading = true;

    //Gọi service 
    this.calendarHttp.addCalendar({
      title: title,
      content: content,
      employeeId: JSON.parse(localStorage.getItem('userData')).employeeId,
      start: startDateTime,
      end: endDateTime
    }).subscribe(
      resData => {
        if (!resData.success) {
          this.error = resData['userMsg']
        } else {
          this.notificationService.addSuccessNotification("Đã thêm sự kiện vào kế hoạch");
        }
        this.isLoading = false;
      },
      error => {
        this.isLoading = false;
        console.log(error);
        this.error = error
      },
    );

    //reset lại form
    form.reset();

    this.baseService.setViewApp("VIEW");
  }

  cancelFormAdd() {
    this.baseService.setViewApp("VIEW");
  }
}
