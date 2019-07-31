import { Injectable } from '@angular/core';
import { storageKey } from './constants';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  
  public changes$:Subject<any> = new Subject();

  public get = (options: { name: string }) => {

    const item = localStorage.getItem(`${storageKey}:${options.name}`);
    
    if(item === 'undefined') return null;

    try {
      return JSON.parse(item);
    } catch (e) {
      return item;
    }
  }

  public put = (options: { name: string, value: any }) => {    
    if((typeof options.value) !== 'string')
      options.value = JSON.stringify(options.value);
    
    localStorage.setItem(`${storageKey}:${options.name}`,options.value);
    this.changes$.next();
  };

  public remove = (options: { name: string }) => {
    localStorage.removeItem(`${storageKey}:${options.name}`);
    this.changes$.next();
  };
}