import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./privacy-page.component.html",
  styleUrls: ["./privacy-page.component.css"],
  selector: "app-privacy-page"
})
export class PrivacyPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
