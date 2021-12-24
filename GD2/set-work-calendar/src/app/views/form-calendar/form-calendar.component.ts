import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { CalendarService } from 'src/app/services/serverHttp/calendar.service';

@Component({
  selector: 'app-form-calendar',
  templateUrl: './form-calendar.component.html',
  styleUrls: ['./form-calendar.component.css']
})
export class FormCalendarComponent implements OnInit {

  calendarForm = new FormGroup({
    title: new FormControl(''),
    start: new FormControl(''),
    end: new FormControl(''),
    content: new FormControl(''),

    checkbox: new FormControl(false),
  });

  constructor(
    private calendarHttp: CalendarService,
  ) { }

  ngOnInit(): void {
  }

  public onSubmit() {
    console.log(this.calendarForm.value);
    this.calendarHttp.addCalendar(this.calendarForm.value).subscribe((data) => {
      console.log(data);
    })
  }
}
