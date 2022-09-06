import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventListComponent } from './event-list/event-list.component';
import { SaveEventComponent } from './save-event/save-event.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { EventComponent } from './event.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { EventRoutingModule } from './event-routing.module';
import { RouterModule } from '@angular/router'
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { TextFieldModule } from '@angular/cdk/text-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';






@NgModule({
  declarations: [
    EventComponent,
    EventListComponent,
    SaveEventComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    EventRoutingModule,
    ToastrModule,
    TextFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatSelectModule
  ],
  exports: [RouterModule],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class EventModule { }
