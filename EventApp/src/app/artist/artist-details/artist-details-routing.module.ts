import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArtistDetailsComponent } from './artist-details.component';

const routes: Routes = [
  {
    path: '',
    component: ArtistDetailsComponent
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ArtistDetailsRoutingModule { }
