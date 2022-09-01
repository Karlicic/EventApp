import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { NavbarLayoutComponent } from './navbar-layout/navbar-layout.component';

const routes: Routes = [
  { path: '', redirectTo: '/events', pathMatch: 'full' },
  {
    path: '',
    component: NavbarLayoutComponent,
    children: [
      { path: 'events', loadChildren: () => import('./event/event.module').then(m => m.EventModule) }
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
