import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CoreModule } from '@kids-toy-hive/core';
import { DomainModule } from '@kids-toy-hive/domain';
import { SharedModule } from '@kids-toy-hive/shared';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { Domain } from 'domain';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    RouterModule.forRoot([], { initialNavigation: 'enabled' }),
    
    CoreModule,
    DomainModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
