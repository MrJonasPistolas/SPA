import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { finalize, map, Subscription } from 'rxjs';

import { Result, TokenResponse } from '../../models';
import { AuthService } from '../../services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  // Properties
  public LoginForm: FormGroup = new FormGroup({
    Email: new FormControl(''),
    Password: new FormControl(''),
    RememberMe: new FormControl(false)
  });
  public Loading: boolean = false;
  public Submitted: boolean = false;
  public ReturnUrl: string = '';
  public LoginError: boolean = false;
  public Subscription: Subscription | null = null;
  public CurrentYear = new Date().getUTCFullYear();
  public Translations = new Array<any>();
  public ErrorsList = new Array<any>();

  // Constructor
  constructor(
    private _FormBuilder: FormBuilder,
    private _ActivatedRoute: ActivatedRoute,
    private _Router: Router,
    private _AuthService: AuthService,
    private _TitleService: Title,
    private _TranslateService: TranslateService,
    private _Loader: NgxUiLoaderService
  ) { }

  ngOnInit(): void {
    this._TranslateService.get('login.pageTitle').subscribe((x: string) => {
      this._TitleService.setTitle(x);
      var languageCode = this._TranslateService.getDefaultLang();
      this.Translations = this._TranslateService.translations[languageCode];
    });

    this.Subscription = this._AuthService.user$.subscribe((x: TokenResponse | null) => {
      if (this._ActivatedRoute.snapshot.url[0].path === 'login') {
        const accessToken = localStorage.getItem('access_token');
        const refreshToken = localStorage.getItem('refresh_token');
        if (x && accessToken && refreshToken) {
          const returnUrl = this._ActivatedRoute.snapshot.queryParams['returnUrl'] || '';
          this._Router.navigate([returnUrl]);
        }
      }
    });

    this.LoginForm = this._FormBuilder.group({
      Email: [
        '',
        [
          Validators.required,
          Validators.email
        ]
      ],
      Password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(40)
        ]
      ],
      RememberMe: [false]
    });
  }

  get f() {
    return this.LoginForm?.controls;
  }

  showPassword(event: Event, element: HTMLElement) {
    if (element.getAttribute('data-password') == 'false') {
      element.previousElementSibling?.setAttribute('type', 'text');
      element.setAttribute('data-password', 'true');
      element.classList.add('show-password');
    } else {
      element.previousElementSibling?.setAttribute('type', 'password');
      element.setAttribute('data-password', 'false');
      element.classList.remove('show-password');
    }
  }

  onSubmit() {
    this.LoginError = false;
    this.Submitted = true;

    if (this.LoginForm.invalid) {
      return;
    }

    this._Loader.startLoader('loader-login');

    const returnUrl = this._ActivatedRoute.snapshot.queryParams['returnUrl'] || '';
    this._AuthService
      .login(this.f['Email'].value, this.f['Password'].value, this.f['RememberMe'].value)
      .pipe(
        finalize(() => {
          this._Loader.stopLoader('loader-login');
          this.Submitted = false;
        })
      )
      .subscribe(
        (r: Result<TokenResponse>) => {
          if (r.succeeded) {
            this._Router.navigate([returnUrl]);
          } else {
            this.ErrorsList = new Array<string>();

            r.messages.forEach((value: string) => {
              this.ErrorsList.push(this.Translations[<any>value]);
            });

            this.LoginError = true;
          }
        },
        (error: any) => {
          this.LoginError = error;
        }
      );
  }

  ngOnDestroy(): void {
    this.Subscription?.unsubscribe();
  }
}
