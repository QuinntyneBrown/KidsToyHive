import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./toys-page.component.html",
  styleUrls: ["./toys-page.component.css"],
  selector: "app-toys-page"
})
export class ToysPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
