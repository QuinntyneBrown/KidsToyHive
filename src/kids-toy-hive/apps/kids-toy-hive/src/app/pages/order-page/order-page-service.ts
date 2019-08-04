import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Product, Booking } from '@kids-toy-hive/domain';

@Injectable()
export class OrderPageService {

    public product$: BehaviorSubject<Product> = new BehaviorSubject(null);
    public booking$: BehaviorSubject<Booking> = new BehaviorSubject(null);
}