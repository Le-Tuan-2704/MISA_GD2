import { Component, forwardRef, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { FullCalendarComponent } from '@fullcalendar/angular';
import { CalendarOptions, Calendar } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
import timeGridPlugin from '@fullcalendar/timegrid';
import { CalendarService } from 'src/app/services/serverHttp/calendar.service';
import { MCalendarCardComponent } from '../mcalendar-card/mcalendar-card.component';


@Component({
  selector: 'app-m-calendar',
  templateUrl: './m-calendar.component.html',
  styleUrls: ['./m-calendar.component.css']
})
export class MCalendarComponent implements OnInit {

  calendarOptions: CalendarOptions;
  eventsModel: any;
  // @ViewChild('fullcalendar') fullcalendar: FullCalendarComponent;
  @ViewChild('fullcalendar', { static: true }) calendarComponent: FullCalendarComponent;
  @ViewChild('createEvent', { static: true }) createEvent: TemplateRef<any>;

  dateForm: FormGroup;
  eventsCalendar: any[] = [];
  events: any[] = [];
  // calendarEvents: EventInput[] = [];
  calendarPlugins = [dayGridPlugin, timeGridPlugin, interactionPlugin];
  calendarApi: Calendar;
  initialized = false;

  constructor(
    private calendarHttp: CalendarService,
    public dialog: MatDialog,
  ) {

  }

  ngOnInit() {
    // cần tải gói lịch trước
    forwardRef(() => Calendar);

    this.calendarOptions = {
      plugins: [dayGridPlugin, timeGridPlugin, interactionPlugin],
      editable: true,
      customButtons: {
        myCustomButton: {
          text: 'custom!',
          click: function () {
            alert('clicked the custom button!');
          }
        }
      },

      headerToolbar: {
        left: 'prev,next today myCustomButton',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay'
      },

      // chuyển đổi các phương thức
      //click vào khung ngày, khung giờ
      dateClick: this.handleDateClick.bind(this),
      //click vào event
      eventClick: this.handleEventClick.bind(this),
      // kéo thả event
      eventDragStop: this.handleEventDragStop.bind(this),

      events: [],
    };

    this.calendarHttp.getCalendar().subscribe((datas) => {
      console.log(datas);
      this.calendarOptions.events = datas;
    })
  }

  handleDateClick(arg) {
    console.log("click 1", arg);
  }

  handleEventClick(arg) {
    console.log("click 2", arg);
    let event = arg.view;
    const dialogRef = this.dialog.open(MCalendarCardComponent, {
      data: { ...event },
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  handleEventDragStop(arg) {
    console.log("click 3", arg);
  }

  updateHeader() {
    this.calendarOptions.headerToolbar = {
      left: 'prev,next myCustomButton',
      center: 'title',
      right: ''
    };
  }

  updateEvents() {
    const nowDate = new Date();
    const yearMonth = nowDate.getUTCFullYear() + '-' + (nowDate.getUTCMonth() + 1);

    this.calendarOptions.events = [{
      title: 'Updaten Event',
      start: yearMonth + '-08',
      end: yearMonth + '-10'
    }];
  }
}
