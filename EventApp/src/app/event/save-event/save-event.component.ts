import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ArtistService } from '../../artist/artist.service';
import { EventListComponent } from '../event-list/event-list.component';
import { EventService } from '../event.sevrice';
import { ToastrService } from 'ngx-toastr';
import { HostService } from '../../host/host.service';
import { IHostNameView } from '../../host/models/host-name-view';
import { IArtistListView } from '../../artist/models/artist-list-view';


@Component({
  selector: 'app-save-event',
  templateUrl: './save-event.component.html',
  styleUrls: ['./save-event.component.css']
})
export class SaveEventComponent implements OnInit {

  modalTitle: string = 'Create Event';

  description!: string;
  price!: number;
  title!: string;
  dateTime!: Date;
  location!: string;
  hosts!: IHostNameView[];
  host!: string;
  artists!: IArtistListView[];
  artist!: string;

  errorStatus: number | undefined;
  saveButtonFlag: boolean = false;


  constructor(private artistService: ArtistService, private eventService: EventService,
    private dialogRef: MatDialogRef<EventListComponent>, private toastrService: ToastrService, private hostService: HostService) { }

  ngOnInit(): void {
    this.loadHosts();
  }

  async saveEvent(): Promise<void> {
    this.saveButtonFlag = true;
    const e = { title: this.title, description: this.description, date: this.dateTime, price: this.price, hostIdentifier: this.host, artistIdentifier: this.artist };
    var response = await this.eventService.createEvent(e);
    //TO DO: Error check
    this.dialogRef.close(true);
  }

  async loadHosts(): Promise<void> {
    var data = await this.hostService.getHosts();
    this.hosts = data as IHostNameView[];
    var response = await this.artistService.getArtists();
    this.artists = response as IArtistListView[];
  }

  changeInfo(event: any) {
    if (this.errorStatus == 400) {
      this.saveButtonFlag = false;
      this.errorStatus = undefined;
    }
  }

  onCancel() {
    this.dialogRef.close();
  }
}
