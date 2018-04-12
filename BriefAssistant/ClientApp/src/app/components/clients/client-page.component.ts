import { Component, OnInit } from '@angular/core';
import { Address } from '../../models/Address';

@Component({
  selector: "client-page",
  templateUrl: "./client-page.component.html"
})

export class ClientPageComponent implements OnInit{
  phone: string;
  email: string;
  address: Address;
  name: string;

  ngOnInit() {

  }
}
