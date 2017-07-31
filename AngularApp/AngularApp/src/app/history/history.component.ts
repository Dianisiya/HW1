import { Component } from '@angular/core';
import { RequestService } from "app/services/requests.service";

@Component({
  selector: 'history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent {
  private historyList;

  constructor(private requests: RequestService){
    requests.get("/Home/GetHistory").subscribe(res =>{
      console.log(res);
      this.historyList = res.map(h => {
        return {
          description: h.Description,
          date: new Date(h.DateTime),
          result: h.Result
        }
      });
    })
  }
}
