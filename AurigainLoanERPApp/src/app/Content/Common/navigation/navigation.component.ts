import { Component, OnInit } from '@angular/core';
import { Routing_Url } from 'src/app/Shared/Helper/constants';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  get routing_Url() { return Routing_Url }

  constructor() { }

  ngOnInit(): void {
    
  }

}
