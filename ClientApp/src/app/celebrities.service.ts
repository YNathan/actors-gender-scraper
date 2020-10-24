import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Celebrity} from "./Celebrity";
import {Observable} from "rxjs";
import {HttpParams} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class CelebritiesService {

  private readonly _http: HttpClient;
  private readonly _baseUrl: string;
  public celebrities$: Observable<Celebrity[]>;
  DATA_FEED_CONTROLLER_NAME: string = "datafeed";
  SCRAPER_CONTROLLER_NAME: string = "scraper";
  public loading: boolean;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
    this._http = http;
  }

  getAllCelebrities() {
    this._http.get<Celebrity[]>(this._baseUrl + this.DATA_FEED_CONTROLLER_NAME).subscribe(celebs => {
      this.celebrities$ = new Observable(subscriber => {
        subscriber.next(celebs)
      });
    });
  }

  deleteCelebritiyByName(name: string) {
    const options = {params: new HttpParams().set('name', name)};
    return this._http.delete<Celebrity[]>(this._baseUrl + this.DATA_FEED_CONTROLLER_NAME, options).subscribe(celebs => {
      this.celebrities$ = new Observable(subscriber => {
        subscriber.next(celebs)
      });
    });
  }

  initializeCelebrity() {
    this.loading = true;
    return this._http.get<Celebrity[]>(this._baseUrl + this.SCRAPER_CONTROLLER_NAME).subscribe(celebs => {
      this.loading = false;
      this.celebrities$ = new Observable(subscriber => {
        subscriber.next(celebs)
      });
    });;
  }

}
