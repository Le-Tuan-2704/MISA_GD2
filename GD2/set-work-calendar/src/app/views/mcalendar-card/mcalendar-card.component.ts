import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CalendarEvent } from 'src/app/models/event.model';

@Component({
  selector: 'app-mcalendar-card',
  templateUrl: './mcalendar-card.component.html',
  styleUrls: ['./mcalendar-card.component.css']
})
export class MCalendarCardComponent implements OnInit {

  public event;

  constructor(
    public dialogRef: MatDialogRef<MCalendarCardComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CalendarEvent,
  ) { }

  ngOnInit(): void {
    this.event = this.data;
    console.log(this.event);

  }

}
