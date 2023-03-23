import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { HtmlContentService, HtmlContent } from '@kids-toy-hive/domain';

@Component({
  templateUrl: './about-page.component.html',
  styleUrls: ['./about-page.component.css'],
  selector: 'kth-about-page'
})
export class AboutPageComponent implements OnDestroy, OnInit  { 
  public onDestroy: Subject<void> = new Subject<void>();
  public htmlContent$:Observable<HtmlContent>;

  constructor(
    private _htmlContentService: HtmlContentService
  ) {

  }

  ngOnInit() {
    this.htmlContent$ = this._htmlContentService.getByName({ name: 'About.html'})
  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
