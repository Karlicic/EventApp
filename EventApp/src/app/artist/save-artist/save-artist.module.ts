import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SaveArtistComponent } from './save-artist.component';
import { SaveArtistRoutingModule } from './save-artist-routing.module';



@NgModule({
  declarations: [
    SaveArtistComponent
  ],
  imports: [
    CommonModule,
    SaveArtistRoutingModule
  ]
})
export class SaveArtistModule { }
