import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ArtistDetailView } from "./models/artist-detail-view";
import { IArtistListView } from "./models/artist-list-view";
import { ICreateArtistView } from "./models/create-artist-view";

const requestOptions: Object = {
  responseType: 'text'
}

@Injectable({
  providedIn: "root"
})
export class ArtistService {

  private artistUrl = "https://localhost:7287/api/artists";

  constructor(private httpClient: HttpClient) { }

  createArtist(artist: ICreateArtistView) {
    return this.httpClient.post<string>(this.artistUrl, artist, requestOptions).toPromise();
  }

  getArtist(id: string | null) {
    return this.httpClient.get<ArtistDetailView>(this.artistUrl+ "/" + id);
  }

  getArtists(): Promise<IArtistListView[] | undefined> {
    return this.httpClient.get<IArtistListView[]>(this.artistUrl).toPromise();
  }

}
