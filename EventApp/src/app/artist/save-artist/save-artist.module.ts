import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SaveArtistComponent } from './save-artist.component';
import { SaveArtistRoutingModule } from './save-artist-routing.module';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    SaveArtistComponent
  ],
  imports: [
    CommonModule,
    SaveArtistRoutingModule,
    FormsModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule
  ]
})
export class SaveArtistModule { }
