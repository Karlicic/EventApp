import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SaveArtistComponent } from './save-artist.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: SaveArtistComponent
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SaveArtistRoutingModule { }
