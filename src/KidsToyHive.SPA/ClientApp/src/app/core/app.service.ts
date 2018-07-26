import { BehaviorSubject } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable()
export class AppService {
  constructor() {

  }

  public checks$: BehaviorSubject<void> = new BehaviorSubject(null);
}
