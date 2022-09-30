import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArtistService } from '../artist.service';
import { ArtistDetailView } from '../models/artist-detail-view';

@Component({
  selector: 'app-artist-details',
  templateUrl: './artist-details.component.html',
  styleUrls: ['./artist-details.component.css']
})
export class ArtistDetailsComponent implements OnInit {

  artist!: ArtistDetailView;

  constructor(private artistService: ArtistService, private route: ActivatedRoute) { }

  async ngOnInit(): Promise<void> {
    var id = this.route.snapshot.paramMap.get('id');

    var result = await this.artistService.getArtist(id).toPromise();
    if (result == undefined) {
      //TODO: thow error
      return;
    }
    this.artist = result;
  }

}
