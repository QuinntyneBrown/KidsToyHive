import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Product, Booking, Customer } from '@kids-toy-hive/domain';
import { baseUrl } from '@kids-toy-hive/core';
import { map } from 'rxjs/operators';

@Injectable()
export class YourOrderService {
    public customer$: BehaviorSubject<Customer> = new BehaviorSubject(null);
    public product$: BehaviorSubject<Product> = new BehaviorSubject(null);
    public booking$: BehaviorSubject<Booking> = new BehaviorSubject(null);
    public bookingTimeSlot$: BehaviorSubject<number> = new BehaviorSubject(null);
    public bookingDate$: BehaviorSubject<string> = new BehaviorSubject(null);
    
    public productImageUrl$: Observable<string> = this.product$
    .pipe(
        map(x => {
            if(x)
                return `${this._baseUrl}${x.productImages[0].url}`;

                return '';
        })
    );

    public subTotal$: Observable<string>;

    public total$: Observable<string>;

    constructor(
        @Inject(baseUrl) private readonly _baseUrl:string
    ) {

    }
}