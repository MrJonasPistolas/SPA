import {
  Component,
  OnDestroy,
  OnInit
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { Title } from '@angular/platform-browser';
import {
  ActivatedRoute,
  Router
} from '@angular/router';
import {
  finalize,
  Subscription
} from 'rxjs';

import { NgxUiLoaderService } from 'ngx-ui-loader';

import {
  Result,
  TokenResponse
} from '../../models';
import { RootScope } from '../../scopes';
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
  public Translations: any = {};
  public ErrorsList = new Array<any>();

  // Constructor
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private titleService: Title,
    private rootScope: RootScope,
    private loader: NgxUiLoaderService
  ) { }

  ngOnInit(): void {
    this.Translations = this.rootScope.GetTranslations();
    this.titleService.setTitle(this.Translations['login']['pageTitle'])

    this.Subscription = this.authService.user$.subscribe((x: TokenResponse | null) => {
      if (this.activatedRoute.snapshot.url[0].path === 'login') {
        const accessToken = localStorage.getItem('access_token');
        const refreshToken = localStorage.getItem('refresh_token');
        if (x && accessToken && refreshToken) {
          const returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '';
          this.router.navigate([returnUrl]);
        }
      }
    });

    this.LoginForm = this.formBuilder.group({
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

    this.loader.startLoader('loader-login');

    const returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '';
    this.authService
      .login(this.f['Email'].value, this.f['Password'].value, this.f['RememberMe'].value)
      .pipe(
        finalize(() => {
          this.loader.stopLoader('loader-login');
          this.Submitted = false;
        })
      )
      .subscribe(
        (r: Result<TokenResponse>) => {
          if (r.succeeded) {
            this.router.navigate([returnUrl]);
          } else {
            this.ErrorsList = new Array<string>();

            r.messages.forEach((value: string) => {
              this.ErrorsList.push(this.Translations[value]);
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
