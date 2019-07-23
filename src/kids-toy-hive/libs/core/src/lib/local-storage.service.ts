import { Injectable } from '@angular/core';
import { storageKey } from './constants';

@Injectable()
export class LocalStorageService {
  
  public get items() {  
    const storageItems = localStorage.getItem(storageKey);

    if (!storageItems) {
      localStorage.setItem(storageKey, JSON.stringify([]));
      return [];
    }
    
    return JSON.parse(storageItems);
  }

  public set items(value: Array<any>) {
    localStorage.setItem(storageKey, JSON.stringify(value));
  }

  public get = (options: { name: string }) => {
    let storageItem = null;
    for (let i = 0; i < this.items.length; i++) {
      if (options.name === this.items[i].name) storageItem = this.items[i].value;
    }
    return storageItem;
  };

  public put = (options: { name: string; value: any }) => {
    let itemExists = false;

    this.items.forEach((item: any) => {
      if (options.name === item.name) {
        itemExists = true;
        item.value = options.value;
      }
    });

    if (!itemExists) {
      let items = this.items;
      items.push({ name: options.name, value: options.value });
      this.items = items;
      items = null;
    }
  };
}