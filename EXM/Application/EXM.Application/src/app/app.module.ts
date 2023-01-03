import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

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
import { RoleGuard } from './guards';
import { CoreModule } from './modules';
import { CallbackPipe } from './pipes';
import { RootScope } from './scopes';
import { HelperService, IncomeCategoriesService } from './services';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    AppLayoutComponent,
    CallbackPipe
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
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
      fgsColor: '#727CF5',
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
    RootScope,
    HelperService,
    IncomeCategoriesService,
    RoleGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
