import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { NgxUiLoaderModule } from 'ngx-ui-loader';

import { AppRoutingModule } from './app-routing.module';

import {
  AppComponent,
  AppLayoutComponent,
  HomeComponent,
  LoginComponent
} from './components';
import { CoreModule } from './modules';
import { RootScope } from './scopes';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    AppLayoutComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule,
    NgxUiLoaderModule.forRoot({
      bgsColor: '#00ACC1',
      bgsOpacity: 0.5,
      bgsPosition: 'bottom-right',
      bgsSize: 30,
      bgsType: 'ball-spin-clockwise',
      blur: 5,
      fgsColor: '#01a9ac',
      fgsPosition: 'center-center',
      fgsSize: 30,
      fgsType: 'rectangle-bounce',
      gap: 24,
      overlayBorderRadius: '0',
      overlayColor: 'rgba(255,255,255,0.8)',
      pbColor: '#01a9ac',
      hasProgressBar: false
    }),
    CoreModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
    RootScope
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
