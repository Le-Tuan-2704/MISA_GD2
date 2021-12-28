import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {

  //Hiện modal
  @Input() isShow = false;

  //Có thay đổi ẩn hiện
  @Output() isShowChange = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  /**
     * Handle ấn vào backdrop
     * @param event 
     */
  onBackdropClick(event) {
    this.closePopup();
  }

  /**
   * Đóng popup
   */
  closePopup() {
    this.isShowChange.emit(false);
  }
}
