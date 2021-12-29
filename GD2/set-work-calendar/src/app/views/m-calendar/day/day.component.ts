import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CalendarOptions } from '@fullcalendar/angular';
import { CalendarService } from 'src/app/services/serverHttp/calendar.service';
import { CONFIG_CALENDAR } from '../base-config-calendar';

@Component({
  selector: 'app-day',
  templateUrl: './day.component.html',
  styleUrls: ['./day.component.css']
})
export class DayComponent implements OnInit {

  @Output() eventClick = new EventEmitter();

  constructor(
    private calendarHttp: CalendarService,
  ) { }

  ngOnInit(): void {
    this.calendarHttp.getCalendar().subscribe((datas) => {
      console.log(datas);
      this.calendarOptions.events = datas;
      this.calendarHttp.events = datas;
    })
  }

  calendarOptions: CalendarOptions = {
    ...CONFIG_CALENDAR,
    initialView: 'timeGridDay',

    customButtons: {
      myCustomButton: {
        text: 'Calendar Day',
        click: function () {
        }
      }
    },

    // // chuyển đổi các phương thức
    // //click vào khung ngày, khung giờ
    // dateClick: this.handleDateClick.bind(this),
    //click vào event
    eventClick: this.handleEventClick.bind(this),
    // // kéo thả event
    // eventDragStop: this.handleEventDragStop.bind(this),
  };

  // handleDateClick(arg) {
  //   console.log("click 1", arg);
  // }

  handleEventClick(arg) {
    this.eventClick.emit(arg);
  }

  // handleEventDragStop(arg) {
  //   console.log("click 3", arg);
  // }

}
