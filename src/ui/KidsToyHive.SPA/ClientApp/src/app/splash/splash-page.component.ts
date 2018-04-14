import { Component } from "@angular/core";
import { Subject } from "rxjs/Subject";

@Component({
  templateUrl: "./splash-page.component.html",
  styleUrls: ["./splash-page.component.css"],
  selector: "app-splash-page"
})
export class SplashPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
