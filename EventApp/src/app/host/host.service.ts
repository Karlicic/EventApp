import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IHostNameView } from './models/host-name-view';
import { ISaveHostView } from './models/save-host-view';

@Injectable({
  providedIn: 'root'
})
export class HostService {

  private hostUrl = "https://localhost:7287/api/hosts"

  constructor(private httpClient: HttpClient) { }

  getHosts(): Promise<IHostNameView[] | undefined> {
    return this.httpClient.get<IHostNameView[]>(this.hostUrl).toPromise();
  }

  //create
  createHost(host: ISaveHostView) {
    return this.httpClient.post(this.hostUrl, host).toPromise();
  }
}
