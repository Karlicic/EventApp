import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HostComponent } from './host.component';
import { SaveHostModule } from './save-host/save-host.module';
import { HostRoutingModule } from './host-routing.module';



@NgModule({
  declarations: [
    HostComponent
  ],
  imports: [
    CommonModule,
    SaveHostModule,
    HostRoutingModule
  ]
})
export class HostModule { }
