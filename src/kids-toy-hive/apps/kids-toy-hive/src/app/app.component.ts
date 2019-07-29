import { Component, Inject } from '@angular/core';
import { baseUrl } from '@kids-toy-hive/core';

@Component({
  selector: 'kth-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(
    @Inject(baseUrl)public apiBaseUrl:string
  ) { }

  public get imageUrl() {
    return `${this.apiBaseUrl}api/digitalassets/serve/file/Logo.png`;
  }
}