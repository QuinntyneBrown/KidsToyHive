import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./pricing-page.component.html",
  styleUrls: ["./pricing-page.component.css"],
  selector: "app-pricing-page"
})
export class PricingPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
