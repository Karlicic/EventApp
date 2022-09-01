import { Component, OnInit } from '@angular/core';
import { EventService } from '../event.sevrice';
import { MatDialog } from '@angular/material/dialog';
import { SaveEventComponent } from '../save-event/save-event.component';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css']
})
export class EventListComponent implements OnInit {

  title: string = 'Events';

  filter: string = '';

  constructor(private eventService: EventService, private matDialog: MatDialog) { }

  ngOnInit(): void {
  }

  createEvent(): void {
    let dialogRef = this.matDialog.open(SaveEventComponent, {
      width: '1000px',
      height: '750px'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {

      }
    })
  }
}
