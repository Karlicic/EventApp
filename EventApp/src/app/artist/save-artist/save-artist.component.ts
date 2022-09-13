import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ArtistService } from '../artist.service';

@Component({
  selector: 'app-save-artist',
  templateUrl: './save-artist.component.html',
  styleUrls: ['./save-artist.component.css']
})
export class SaveArtistComponent implements OnInit {

  modalTitle = "Artist sign up";
  name!: string;
  saveButtonFlag: boolean = false;
  errorStatus!: any;

  constructor(private router: Router, private artistService: ArtistService) { }

  ngOnInit(): void {
  }

  async saveArtist(): Promise<void> {
    this.saveButtonFlag = true;
    const artist = { name: this.name };
    var response = this.artistService.createArtist(artist);
    //TODO: Error checking
    this.router.navigate(['/events']);
  }

  changeInfo(event: any) {
    if (this.errorStatus == 400) {
      this.saveButtonFlag = false;
      this.errorStatus = undefined;
    }
  }

  onCancel() {
    this.router.navigate(['/events'])
  }

}
