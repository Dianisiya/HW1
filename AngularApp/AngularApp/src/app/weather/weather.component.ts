import { Component } from '@angular/core';
import { RequestService } from "app/services/requests.service";

@Component({
  selector: 'weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.css']
})
export class WeatherComponent {
  private cities;
  private days = 1;
  private weatherList;
  private city = "Lviv";

  private setTime(days){
    this.days = days;
  }

  private getWeather(){
    console.log(this.days);
    switch(this.days){
      case 1:
        this.requests.get("/Home/GetWeatherNowJson?city=" + this.city).subscribe(res =>{
          this.weatherList = [{
            city: res.Name,
            min: res.Main.Temp,
            max: res.Main.Temp,
            date: new Date(res.Dt*1000),
            icon: `http://openweathermap.org/img/w/${res.Weather[0].Icon}.png`
          }];
        })
        break;
      case 3:
        this.requests.get("/Home/GetWeatherThreeDaysJson?city=" + this.city).subscribe(res =>{
          this.weatherList = res.List.map(w => {
            return {
            city: res.City.Name,
            min: w.Temp.Min,
            max: w.Temp.Max,
            date: new Date(w.Dt*1000),
            icon: `http://openweathermap.org/img/w/${w.Weather[0].Icon}.png`
          };
          })
        })
      break;
      case 7:
        this.requests.get("/Home/GetWeatherSevenDaysJson?city=" + this.city).subscribe(res =>{
          this.weatherList = res.List.map(w => {
            return {
            city: res.City.Name,
            min: w.Temp.Min,
            max: w.Temp.Max,
            date: new Date(w.Dt*1000),
            icon: `http://openweathermap.org/img/w/${w.Weather[0].Icon}.png`
          };
          })
        })
      break;
    }
  }

  constructor(private requests: RequestService){
    requests.get("/Home/GetCities").subscribe(res =>{
      console.log(res);
      this.cities = res;
    })
  }
  
}
