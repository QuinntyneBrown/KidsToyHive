import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AboutPageComponent, HomePageComponent, OrderPageComponent, ConfirmationPageComponent } from './pages';
import { AppRoutingModule } from './app-routing.module';
import { CoreModule, baseUrl } from '@kids-toy-hive/core';
import { DomainModule } from '@kids-toy-hive/domain';
import { SharedModule } from '@kids-toy-hive/shared';
import { environment } from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    AboutPageComponent,
    ConfirmationPageComponent,
    HomePageComponent,
    OrderPageComponent,
  ],
  imports: [
    AppRoutingModule,

    CoreModule,
    DomainModule,
    SharedModule,
    BrowserModule
  ],
  providers: [
    {provide: baseUrl, useValue: environment.baseUrl }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
