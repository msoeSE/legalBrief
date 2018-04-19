import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { PagerService } from '../briefsList/services/pager.service';
import { IContactList } from '../../models/IContactList';
import { ClientService } from '../../services/client.service';
import { IContactListItem } from '../../models/IContactListItem';

@Component({
  selector: "clients",
  templateUrl: "./client-list.component.html"
})

export class ClientListComponent implements OnInit {
  contactList: IContactList;
  list: IContactListItem[];

  constructor(
    private readonly router: Router,
    private readonly clientService: ClientService,
    private readonly http: HttpClient,
    private readonly pagerService: PagerService
  ) { }

  // pager object
  pager: any = {};

  // paged items
  pagedItems: IContactListItem[];

  ngOnInit() {
    this.clientService
      .getContactList()
      .subscribe(contactList => {
        if (contactList != null) {
          this.contactList = contactList;
          this.list = this.contactList.clients;

          // initialize to page 1
          this.setPage(1);
        }
      });
  }

  setPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    // get pager object from service
    this.pager = this.pagerService.getPager(this.list.length, page);

    // get current page of items
    this.pagedItems = this.list.slice(this.pager.startIndex, this.pager.endIndex + 1);
  }
}
