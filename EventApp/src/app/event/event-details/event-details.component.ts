import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { EventListComponent } from '../event-list/event-list.component';
import { IEventDetailsView } from '../models/event-details-view';

@Component({
  selector: 'app-event-details',
  templateUrl: './event-details.component.html',
  styleUrls: ['./event-details.component.css']
})
export class EventDetailsComponent implements OnInit {

  eventDetails!: IEventDetailsView | undefined;

  constructor(private dialogRef: MatDialogRef<EventListComponent>) { }

  ngOnInit(): void {
  }

  close() {
    this.dialogRef.close();
  }

}
