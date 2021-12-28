import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FullCalendarModule } from '@fullcalendar/angular';
import { AppRoutingModule } from './routers/app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';

import dayGridPlugin from '@fullcalendar/daygrid'; // a plugin!
import interactionPlugin from '@fullcalendar/interaction'; // a plugin!
import timeGridPlugin from '@fullcalendar/timegrid';

import { MCalendarComponent } from './views/m-calendar/m-calendar.component';
import { MLoadingComponent } from './components/base/m-loading/m-loading.component';
import { MPopupComponent } from './components/base/m-popup/m-popup.component';
import { HomeComponent } from './views/home/home.component';
import { NotFoundComponent } from './views/not-found/not-found.component';
import { TheLayoutComponent } from './components/the-layout/the-layout.component';
import { TheHeaderComponent } from './components/layout/the-header/the-header.component';
import { TheMenuComponent } from './components/layout/the-menu/the-menu.component';
import { TheContentComponent } from './components/layout/the-content/the-content.component';
import { FormCalendarComponent } from './views/form-calendar/form-calendar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MCalendarCardComponent } from './views/mcalendar-card/mcalendar-card.component';
import { FormsModule } from '@angular/forms';

import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { AuthComponent } from './views/auth/auth.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';

import { LoadingSpinnerComponent } from './components/base/loading-spinner/loading-spinner.component';
import { NotificationComponent } from './components/base/notification/notification.component';
import { ModalComponent } from './components/base/modal/modal.component';
import { AuthInterceptorService } from './services/auth-interceptor.service';
import { TheFuntionComponent } from './components/layout/the-funtion/the-funtion.component';
import { TheBodyComponent } from './components/layout/the-body/the-body.component';
import { TheSideBarLeftComponent } from './components/layout/the-side-bar-left/the-side-bar-left.component';
import { TheSideBarRightComponent } from './components/layout/the-side-bar-right/the-side-bar-right.component';

FullCalendarModule.registerPlugins([ // register FullCalendar plugins
  dayGridPlugin,
  interactionPlugin,
  timeGridPlugin,
]);

@NgModule({
  declarations: [
    AppComponent,
    MCalendarComponent,
    MLoadingComponent,
    MPopupComponent,
    HomeComponent,
    NotFoundComponent,
    TheLayoutComponent,
    TheHeaderComponent,
    TheMenuComponent,
    TheContentComponent,
    FormCalendarComponent,
    MCalendarCardComponent,
    AuthComponent,
    LoadingSpinnerComponent,
    NotificationComponent,
    ModalComponent,
    TheFuntionComponent,
    TheBodyComponent,
    TheSideBarLeftComponent,
    TheSideBarRightComponent,


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FullCalendarModule, // register FullCalendar with you app
    HttpClientModule, // import HttpClientModule after BrowserModule.
    ReactiveFormsModule,
    BrowserAnimationsModule,
    FormsModule,

    MatDialogModule,
    MatButtonModule,
    MatDatepickerModule,
    MatFormFieldModule,
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
