import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArtistComponent } from './artist.component';
import { SaveArtistModule } from './save-artist/save-artist.module';
import { ArtistRoutingModule } from './artist-routing.module';
import { ArtistDetailsModule } from './artist-details/artist-details.module';



@NgModule({
  declarations: [
    ArtistComponent
  ],
  imports: [
    CommonModule,
    SaveArtistModule,
    ArtistDetailsModule,
    ArtistRoutingModule
  ]
})
export class ArtistModule { }
