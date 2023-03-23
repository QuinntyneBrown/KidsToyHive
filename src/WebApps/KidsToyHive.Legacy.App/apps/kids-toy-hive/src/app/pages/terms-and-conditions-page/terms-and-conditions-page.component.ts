// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { HtmlContent, HtmlContentService } from '@kids-toy-hive/domain';

@Component({
  templateUrl: './terms-and-conditions-page.component.html',
  styleUrls: ['./terms-and-conditions-page.component.css'],
  selector: 'kth-terms-and-conditions-page'
})
export class TermsAndConditionsPageComponent implements OnDestroy, OnInit  { 
  public onDestroy: Subject<void> = new Subject<void>();
  public htmlContent$: Observable<HtmlContent>;

  constructor(
    private readonly _htmlContentService: HtmlContentService
  ) {
    
  }

  public ngOnInit() {
    this.htmlContent$ = this._htmlContentService.getByName({name:'TermsAndConditions.html'});
  }
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

