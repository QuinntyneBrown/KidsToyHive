import { OverlayRef } from '@angular/cdk/overlay';
import { BehaviorSubject, Subject, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { Logger } from './logger';

@Injectable()
export class OverlayRefWrapper {
    public result$: BehaviorSubject<any> = new BehaviorSubject(null);
    private _afterClosed = new Subject<any>();

    constructor(private overlayRef: OverlayRef, logger: Logger) { }

    close(result: any = null): void {    
        this.overlayRef.dispose();
        this.result$.next(result);
        this._afterClosed.next(result);
    }

    public afterClosed(): Observable<any> {
        return this._afterClosed;
    }
}