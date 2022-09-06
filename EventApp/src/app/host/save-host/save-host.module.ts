import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SaveHostComponent } from './save-host.component';
import { SaveHostRoutingModule } from './save-host-routing.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';



@NgModule({
  declarations: [
    SaveHostComponent
  ],
  imports: [
    CommonModule,
    SaveHostRoutingModule,
    FormsModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule
  ]
})
export class SaveHostModule { }
