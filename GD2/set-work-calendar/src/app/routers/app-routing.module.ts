import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TheBodyComponent } from '../components/layout/the-body/the-body.component';
import { AuthComponent } from '../views/auth/auth.component';
import { FormCalendarComponent } from '../views/form-calendar/form-calendar.component';
import { HomeComponent } from '../views/home/home.component';
import { NotFoundComponent } from '../views/not-found/not-found.component';
import { AnonymousGuard } from './anonymous.guard';
import { AuthGuard } from './auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'auth', component: AuthComponent, canActivate: [AnonymousGuard] },
  { path: 'home', component: HomeComponent },
  { path: 'calendar', canActivate: [AuthGuard], component: TheBodyComponent },
  { path: 'form', component: FormCalendarComponent },
  // {
  //   path: 'pending', component: PendingViewComponent,
  //   canActivate: [AuthGuard],
  //   data: {
  //     role: '1'
  //   }
  // },

  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
