import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-the-header',
  templateUrl: './the-header.component.html',
  styleUrls: ['./the-header.component.css', '../../../../css/icon.css']
})
export class TheHeaderComponent implements OnInit {

  isAuthenticated = false;

  private userSub: Subscription;

  user: User;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    //subscribe user trong auth service
    this.userSub = this.authService.user.subscribe(user => {
      this.isAuthenticated = !!user;
      this.user = user;
    });
  }

  /**
     * Hàm handle login
     */
  onLogin() {
    this.router.navigate(['/auth']);
  }

  /**
   * Hàm handle logout
   */
  onLogout() {
    this.authService.logout();
  }
}
