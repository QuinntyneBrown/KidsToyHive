import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./footer.component.html",
  styleUrls: ["./footer.component.css"],
  selector: "app-footer"
})
export class FooterComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
