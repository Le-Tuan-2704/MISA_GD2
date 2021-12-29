import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/serverHttp/auth.service';

@Component({
  selector: 'app-the-menu',
  templateUrl: './the-menu.component.html',
  styleUrls: ['./the-menu.component.css']
})
export class TheMenuComponent implements OnInit {
  isAuthenticated: boolean;

  isManager: boolean;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.user.subscribe(user => {
      this.isAuthenticated = !!user;
      this.isManager = user?.role == 1;
    })
  }

}
