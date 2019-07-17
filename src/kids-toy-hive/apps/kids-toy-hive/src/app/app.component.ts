import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor() {

  }
}

export default function wrap(component) {
  return class extends HTMLElement {
    
  }
}

customElements.define("ce-something",wrap(null));