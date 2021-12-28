import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-m-popup',
  templateUrl: './m-popup.component.html',
  styleUrls: ['./m-popup.component.css']
})
export class MPopupComponent implements OnInit {
  @Input() message: string = "";
  @Input() type: string = "success"; //success | warning | confirm  
  @Input() isShow: boolean = false;
  @Input() isLoading: boolean = false;

  @Output() confirmBtnClick = new EventEmitter();
  @Output() closeBtnClick = new EventEmitter();
  constructor() { }

  //Màu của icon
  get iconColor(): string {
    switch (this.type) {
      case "success":
        return "green"
      case "warning":
        return "orangered"
      case "confirm":
        return "blue"
      default:
        return ""
    }
  }

  ngOnInit(): void {
  }

}
