import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-the-side-bar-right',
  templateUrl: './the-side-bar-right.component.html',
  styleUrls: ['./the-side-bar-right.component.css']
})
export class TheSideBarRightComponent implements OnInit {
  isSideRight = false;
  constructor() { }

  ngOnInit(): void {
  }

}
