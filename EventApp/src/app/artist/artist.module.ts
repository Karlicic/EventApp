import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArtistComponent } from './artist.component';
import { SaveArtistModule } from './save-artist/save-artist.module';
import { ArtistRoutingModule } from './artist-routing.module';
import { ArtistDetailsComponent } from './artist-details/artist-details.component';



@NgModule({
  declarations: [
    ArtistComponent,
    ArtistDetailsComponent
  ],
  imports: [
    CommonModule,
    SaveArtistModule,
    ArtistRoutingModule
  ]
})
export class ArtistModule { }
