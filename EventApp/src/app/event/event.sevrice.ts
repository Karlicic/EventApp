import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IPaginatedEventView } from "./models/paginated-event-view";
import { ISaveEventView } from "./models/save-event-view";

@Injectable({
  providedIn: "root"
})
export class EventService {

  private eventUrl = "https://localhost:7218/api/events";

  constructor(private httpClient: HttpClient) { }

  //get events
  getEvents(filter = '', sortBy = 'date', sortOrder = 'asc', pageNumber = 0, pageSize = 3) {
    var params: HttpParams = new HttpParams()
      .set('filter', filter)
      .set('sortBy', sortBy)
      .set('sortOrder', sortOrder)
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());
    return this.httpClient.get<IPaginatedEventView>(this.eventUrl, { params }).toPromise();

  }

  //get event by id

  //create
  createEvent(event: ISaveEventView) {
    return this.httpClient.post(this.eventUrl, event);
  }

  //update

}
