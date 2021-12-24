import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from '../views/auth/auth.component';
import { FormCalendarComponent } from '../views/form-calendar/form-calendar.component';
import { HomeComponent } from '../views/home/home.component';
import { MCalendarComponent } from '../views/m-calendar/m-calendar.component';
import { NotFoundComponent } from '../views/not-found/not-found.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'calendar', component: MCalendarComponent },
  { path: 'form', component: FormCalendarComponent },
  { path: 'login', component: AuthComponent },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
