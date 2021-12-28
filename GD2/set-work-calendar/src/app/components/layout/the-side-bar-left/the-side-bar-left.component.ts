import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-the-side-bar-left',
  templateUrl: './the-side-bar-left.component.html',
  styleUrls: ['./the-side-bar-left.component.css']
})
export class TheSideBarLeftComponent implements OnInit {

  isSideLeft = false;

  constructor() { }

  ngOnInit(): void {
  }

}
