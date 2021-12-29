import { Component, OnInit } from '@angular/core';
import { BaseServiceService } from 'src/app/services/baseService/base-service.service';

@Component({
  selector: 'app-the-body',
  templateUrl: './the-body.component.html',
  styleUrls: ['./the-body.component.css']
})
export class TheBodyComponent implements OnInit {
  viewApp = "VIEW";
  constructor(private baseService: BaseServiceService) { }

  ngOnInit(): void {
    this.baseService.viewApp.subscribe((view) => {
      this.viewApp = view;
    })
  }

  /**
     * form thành công
     */
  onAddFromSuccess() {
    // this.calendarService.reloadEvents();
  }
}
