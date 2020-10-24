import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import {CelebritiesViewComponent} from "./celebrities-view/celebrities-view.component";
import {TranformArrayPipe} from "./tranform-array.pipe";
import {GenderFromRolesPipe} from "./gender-from-roles.pipe";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CelebritiesViewComponent,
    TranformArrayPipe,
    GenderFromRolesPipe,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([    ])
  ],
  providers: [TranformArrayPipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
