import { NgModule } from '@angular/core';
import { HostComponent } from './host.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: HostComponent,
    children: [
      { path: 'save-host', loadChildren: () => import('./save-host/save-host.module').then(m => m.SaveHostModule) }
    ]
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HostRoutingModule { }
