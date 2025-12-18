// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable } from '@angular/core';
import { storageKey } from './constants';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  
  public changes$:BehaviorSubject<any> = new BehaviorSubject(null);

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
    this.changes$.next(null);
  };

  public remove = (options: { name: string }) => {
    localStorage.removeItem(`${storageKey}:${options.name}`);
    this.changes$.next(null);
  };
}
