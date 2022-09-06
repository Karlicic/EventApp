import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router"
import { HostService } from '../host.service';


@Component({
  selector: 'app-save-host',
  templateUrl: './save-host.component.html',
  styleUrls: ['./save-host.component.css']
})
export class SaveHostComponent implements OnInit {

  modalTitle: string = 'Become a host';
  saveButtonFlag: boolean = false;
  errorStatus!: any;
  name!: string;
  page!: string;
  address!: string;

  constructor(private router: Router, private hostService: HostService) { }

  ngOnInit(): void {
  }

  async saveHost(): Promise<void> {
    this.saveButtonFlag = true;
    const host = { name: this.name, page: this.page, address: this.address };
    var response = await this.hostService.createHost(host);
    //TO DD: Check for error
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
