import { Component } from '@angular/core';
import {CelebritiesService} from "../celebrities.service";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor(public celebService: CelebritiesService) {
  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  refrechCelebrities() {
    this.celebService.initializeCelebrity();
  }
}
