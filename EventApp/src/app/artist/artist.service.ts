import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ICreateArtistView } from "./models/create-artist-view";

@Injectable({
  providedIn: "root"
})
export class ArtistService {

  private artistUrl = "https://localhost:7287/api/artists";

  constructor(private httpClient: HttpClient) { }

  createArtist(artist: ICreateArtistView) {
    return this.httpClient.post(this.artistUrl, artist).toPromise();
  }

}
