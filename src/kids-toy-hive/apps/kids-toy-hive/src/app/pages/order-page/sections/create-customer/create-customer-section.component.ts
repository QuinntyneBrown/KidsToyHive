import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { CustomerService } from '@kids-toy-hive/domain';
import { LocalStorageService } from '@kids-toy-hive/core';
import { Router } from '@angular/router';

@Component({
  templateUrl: './create-customer-section.component.html',
  styleUrls: ['./create-customer-section.component.css'],
  selector: 'kth-create-customer-section'
})
export class CreateCustomerSectionComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(
    private readonly _customerService: CustomerService,
    private readonly _localStorageService: LocalStorageService,
    private readonly _router: Router,
  ) {

  }
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
