import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CalendarOptions } from '@fullcalendar/angular';
import { CalendarService } from 'src/app/services/serverHttp/calendar.service';
import { CONFIG_CALENDAR } from '../base-config-calendar';

@Component({
  selector: 'app-week',
  templateUrl: './week.component.html',
  styleUrls: ['./week.component.css']
})
export class WeekComponent implements OnInit {
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
    initialView: 'timeGridWeek',

    customButtons: {
      myCustomButton: {
        text: 'Calendar Week',
        click: function () {
        }
      }
    },

    // chuyển đổi các phương thức
    //click vào khung ngày, khung giờ
    dateClick: this.handleDateClick.bind(this),
    //click vào event
    eventClick: this.handleEventClick.bind(this),
    // kéo thả event
    eventDragStop: this.handleEventDragStop.bind(this),
  };

  handleDateClick(arg) {
    console.log("click 1", arg);
  }

  handleEventClick(arg) {
    this.eventClick.emit(arg);
  }

  handleEventDragStop(arg) {
    console.log("click 3", arg);
  }

}
