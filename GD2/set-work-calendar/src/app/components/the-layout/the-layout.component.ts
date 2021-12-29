import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/serverHttp/auth.service';

@Component({
  selector: 'app-the-layout',
  templateUrl: './the-layout.component.html',
  styleUrls: ['./the-layout.component.css']
})
export class TheLayoutComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    //Tự động login khi khởi chạy
    this.authService.autoLogin();
  }

}
