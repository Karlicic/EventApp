import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SaveHostComponent } from './save-host.component';

const routes: Routes = [
  {
    path: '',
    component: SaveHostComponent
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SaveHostRoutingModule { }
