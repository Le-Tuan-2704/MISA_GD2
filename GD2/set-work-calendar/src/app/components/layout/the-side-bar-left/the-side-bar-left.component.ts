import { Component, OnInit } from '@angular/core';
import { BaseServiceService } from 'src/app/services/baseService/base-service.service';

@Component({
  selector: 'app-the-side-bar-left',
  templateUrl: './the-side-bar-left.component.html',
  styleUrls: ['./the-side-bar-left.component.css']
})
export class TheSideBarLeftComponent implements OnInit {

  isSideLeft = true;
  viewCalendar = "MONTH";

  constructor(private baseService: BaseServiceService) { }

  ngOnInit(): void {
    this.baseService.viewCalendar.subscribe((view) => {
      this.viewCalendar = view;
    })
  }

  onClickItemMenu(typeCalendar: string) {
    this.baseService.setViewCalendar(typeCalendar);
  }
}
