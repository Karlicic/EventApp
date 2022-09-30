import { Component, OnInit } from '@angular/core';
import { EventService } from '../event.sevrice';
import { MatDialog } from '@angular/material/dialog';
import { SaveEventComponent } from '../save-event/save-event.component';
import { IEventListView } from '../models/event-list-view';
import { EventDetailsComponent } from '../event-details/event-details.component';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css']
})
export class EventListComponent implements OnInit {

  title: string = 'Events';
  filter: string = '';
  events!: IEventListView[];

  constructor(private eventService: EventService, private matDialog: MatDialog) { }

  ngOnInit(): void {
    this.loadEventList();
  }

  async loadEventList(): Promise<IEventListView[] | undefined> {
    var response = await this.eventService.getEvents();

    if (response == undefined) {
      //TODO: Error handling
      return response;
    }
    this.events = response;
    return this.events;

  }

  async viewEvent(identifier?: string) {
    let dialogRef = this.matDialog.open(EventDetailsComponent, {
      width: '600px',
      height: '350px'
    });
    let id = identifier?.substring(identifier.lastIndexOf('/') + 1);
    if (id == undefined) {
      return;
    }
    let myEvent = await this.eventService.getEvent(id);
    dialogRef.componentInstance.eventDetails = myEvent;

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadEventList();
      }
    });
  }

  createEvent(): void {
    let dialogRef = this.matDialog.open(SaveEventComponent, {
      width: '800px',
      height: '650px'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadEventList();
      }
    });
  }
}
