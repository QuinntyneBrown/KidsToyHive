// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable } from '@angular/core';
import { ILogger, LogLevel } from '@microsoft/signalr';
import { LocalStorageService } from './local-storage.service';

@Injectable()
export class Logger implements ILogger {
    public minimumLogLevel: LogLevel = 0;
    public apiUrl: string;
    
    constructor(private readonly _storage: LocalStorageService) { }

  log(logLevel: LogLevel, message: string): void {
    
    if (logLevel < this.minimumLogLevel) return;
    
    switch (logLevel) {
      case LogLevel.Critical:
      case LogLevel.Error:
        console.error(`${LogLevel[logLevel]}: ${message}`);        
        break;
      case LogLevel.Warning:
        console.warn(`${LogLevel[logLevel]}: ${message}`);
        break;
      case LogLevel.Information:
        console.info(`${LogLevel[logLevel]}: ${message}`);        
        break;
      case LogLevel.Debug:
        console.debug(`${LogLevel[logLevel]}: ${message}`);
        break;
      default:
        console.log(`${LogLevel[logLevel]}: ${message}`);
        break;
    }
  }
  
  public trace(title: string, message: string) {
    this.log(LogLevel.Trace, `(${title}) ${message}`);
  }

  public error(title: string, message: string) {
    this.log(LogLevel.Error, `(${title}) ${message}`);
  }
}

