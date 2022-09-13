import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArtistDetailsComponent } from './artist-details.component';
import { ArtistDetailsRoutingModule } from './artist-details-routing.module';



@NgModule({
  declarations: [
    ArtistDetailsComponent
  ],
  imports: [
    CommonModule,
    ArtistDetailsRoutingModule
  ]
})
export class ArtistDetailsModule { }
