import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { TranslateLoader, TranslateModule, TranslateService } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { DataTablesModule } from 'angular-datatables'

import { AppRoutingModule } from './app-routing.module';

import {
  AppComponent,
  AppLayoutComponent,
  HomeComponent,
  IncomeCategoriesComponent,
  LoginComponent
} from './components';
import { RoleGuard } from './guards';
import { CallbackPipe } from './pipes';
import { RootScope } from './scopes';
import {
  AuthService,
  HelperService,
  IncomeCategoriesService
} from './services';
import { AppInitializer } from './initializer';
import {
  JwtInterceptor,
  UnauthorizedInterceptor
} from './interceptors';
import { MainLayoutEmit } from './emits';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    AppLayoutComponent,
    IncomeCategoriesComponent,
    CallbackPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule,
    DataTablesModule,
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
    TranslateModule.forRoot()
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: AppInitializer,
      multi: true,
      deps: [
        AuthService,
        HttpClient,
        TranslateService,
        RootScope
      ],
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: UnauthorizedInterceptor,
      multi: true,
    },
    MainLayoutEmit,
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
