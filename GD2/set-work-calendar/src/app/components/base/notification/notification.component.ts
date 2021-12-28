import { Component, OnInit } from '@angular/core';
import { Notification } from 'src/app/models/notification.model';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {

  //danh sách thông báo
  notiList: Notification[];

  constructor(private notificationService: NotificationService) { }

  ngOnInit(): void {
    //Lấy danh sách thông báo
    this.notificationService.notiSources.subscribe(
      notiList => {
        this.notiList = notiList
      }
    )
  }

}
