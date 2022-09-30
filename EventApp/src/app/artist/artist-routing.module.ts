import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArtistComponent } from './artist.component';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  {
    path: '',
    component: ArtistComponent,
    children: [
      { path: 'save-artist', loadChildren: () => import('./save-artist/save-artist.module').then(m => m.SaveArtistModule) },
      { path: ':id', loadChildren: () => import('./artist-details/artist-details.module').then(m => m.ArtistDetailsModule) }
    ]
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ArtistRoutingModule { }
