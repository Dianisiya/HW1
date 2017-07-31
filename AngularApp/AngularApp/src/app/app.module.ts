import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Router } from "@angular/router"

import { AppComponent } from './app/app.component';
import { MainComponent } from "app/main/main.component";
import { WeatherComponent } from "app/weather/weather.component";
import { HistoryComponent } from "app/history/history.component";
import { RequestService, requestServiceFactory } from "app/services/requests.service";
import { Http, JsonpModule, HttpModule } from "@angular/http";
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    WeatherComponent,
    HistoryComponent
  ],
  imports: [
    HttpModule,
    JsonpModule,
    BrowserModule,
    FormsModule,
    RouterModule.forRoot([
      {path: "main", component: MainComponent},
      {path: "weather", component: WeatherComponent},
      {path: "history", component: HistoryComponent},
      {path: "**", redirectTo: "main", pathMatch: "full"}
    ])
  ],
  providers: [{provide: RequestService, useFactory: requestServiceFactory, deps: [Http, Router] }],
  bootstrap: [AppComponent]
})
export class AppModule { }
