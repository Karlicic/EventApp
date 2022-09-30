import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IEventDetailsView } from "./models/event-details-view";
import { IEventListView } from "./models/event-list-view";
import { ISaveEventView } from "./models/save-event-view";

@Injectable({
  providedIn: "root"
})
export class EventService {

  private eventUrl = "https://localhost:7287/api/events";

  constructor(private httpClient: HttpClient) { }

  //get events
  getEvents() {
    return this.httpClient.get<IEventListView[] | undefined>(this.eventUrl).toPromise();
  }

  getEvent(identifier: string) {
    return this.httpClient.get<IEventDetailsView>(this.eventUrl + '/' + identifier).toPromise();
  }

  //create
  createEvent(event: ISaveEventView) {
    return this.httpClient.post(this.eventUrl, event).toPromise();
  }

}
