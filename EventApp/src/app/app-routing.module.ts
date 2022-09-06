import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavbarLayoutComponent } from './navbar-layout/navbar-layout.component';

const routes: Routes = [
  { path: '', redirectTo: '/events', pathMatch: 'full' },
  {
    path: '',
    component: NavbarLayoutComponent,
    children: [
      { path: 'events', loadChildren: () => import('./event/event.module').then(m => m.EventModule) },
      { path: 'hosts', loadChildren: () => import('./host/host.module').then(m => m.HostModule) },
      { path: 'artists', loadChildren: () => import('./artist/artist.module').then(m => m.ArtistModule) }
    ]
  }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
