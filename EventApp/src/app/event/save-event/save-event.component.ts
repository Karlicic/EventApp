import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ArtistService } from '../../artist/artist.service';
import { EventListComponent } from '../event-list/event-list.component';
import { EventService } from '../event.sevrice';
import { IArtistsStageName } from '../models/artist-stage-name';

@Component({
  selector: 'app-save-event',
  templateUrl: './save-event.component.html',
  styleUrls: ['./save-event.component.css']
})
export class SaveEventComponent implements OnInit {

  title: string = 'Create Event';
  artists: IArtistsStageName[] = [];

  description!: string;
  price!: number;
  errorStatus: number | undefined;
  saveButtonFlag: boolean = false;


  constructor(private artistService: ArtistService, private eventService: EventService,
    private dialogRef: MatDialogRef<EventListComponent>, private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  async saveEvent(): Promise<void> {
    this.saveButtonFlag = true;
    const e = { id: 0, price: this.price, description: this.description }
    var response = await this.eventService.createEvent(e);
    //if (response.hasError) {
    //  this.toastrService.error(response.error.error.detail);
    //}
    //this.toastrService.success('Transaction was successfully added!');
    this.dialogRef.close(true);
  }

}
