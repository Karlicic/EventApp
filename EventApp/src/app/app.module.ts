import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NavbarLayoutComponent } from './navbar-layout/navbar-layout.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { MatMenuModule } from '@angular/material/menu';
import { EventModule } from './event/event.module';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { HostModule } from './host/host.module';
import { ArtistModule } from './artist/artist.module';




@NgModule({
  declarations: [
    AppComponent,
    NavbarLayoutComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NoopAnimationsModule,
    AppRoutingModule,
    MatMenuModule,
    EventModule,
    HostModule,
    ArtistModule,
    RouterModule,
    MatNativeDateModule,
    MatButtonModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      closeButton: true
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
