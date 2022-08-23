import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class ArtistService {

  private artistUrl = "https://localhost:7218/api/artists";

  constructor(private httpClient: HttpClient) { }


}
