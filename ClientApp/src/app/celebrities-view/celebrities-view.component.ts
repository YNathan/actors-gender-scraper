import {Component, OnInit} from '@angular/core';
import {CelebritiesService} from "../celebrities.service";
import {Observable} from "rxjs";
import {Celebrity} from "../Celebrity";

@Component({
  selector: 'app-celebrities-view',
  templateUrl: './celebrities-view.component.html',
  styleUrls: ['./celebrities-view.component.css']
})
export class CelebritiesViewComponent implements OnInit {

  celebrities$: Observable<Celebrity[]>;

  constructor(public celebService: CelebritiesService) {
  }

  ngOnInit() {
    this.celebrities$ = this.celebService.celebrities$;
    this.celebService.getAllCelebrities();
  }

  removeCeleb(name: string) {
    this.celebService.deleteCelebritiyByName(name);
  }

}
