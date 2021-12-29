import { Component, OnInit } from '@angular/core';
import { BaseServiceService } from 'src/app/services/baseService/base-service.service';

@Component({
  selector: 'app-the-funtion',
  templateUrl: './the-funtion.component.html',
  styleUrls: ['./the-funtion.component.css']
})
export class TheFuntionComponent implements OnInit {
  showFunction = false;
  constructor(private baseService: BaseServiceService) { }

  ngOnInit(): void {
  }

  /**
   * Thêm mới event
   */
  addEvent() {
    this.baseService.setViewApp("ADD");
  }
}
