import { HttpClient } from "@angular/common/http";
import { Injectable, OnDestroy } from "@angular/core";
import { Router } from "@angular/router";
import {
  BehaviorSubject,
  catchError,
  delay,
  finalize,
  map,
  Observable,
  of,
  Subscription,
  tap
} from "rxjs";
import { environment } from "src/environments/environment";

// 3rd party libraries
import {
  Result,
  TokenResponse,
  RefreshTokenRequest,
  TokenRequest
} from "../models";
import { RootScope } from "../scopes";

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  private readonly apiUrl = `${environment.serverUrl}`;
  private timer: Subscription | null = null;
  private _user = new BehaviorSubject<TokenResponse | null>(null);
  user$ = this._user.asObservable();

  constructor(
    private router: Router,
    private http: HttpClient,
    private rootScope: RootScope) {
    window.addEventListener('storage', this.storageEventListener.bind(this));
  }

  private storageEventListener(event: StorageEvent) {
    if (event.storageArea === localStorage) {
      if (event.key === 'logout-event') {
        this.stopTokenTimer();
        this._user.next(null);
      }

      if (event.key === 'login-event') {
        this.stopTokenTimer();
        //this.http.post
      }
    }
  }

  ngOnDestroy(): void {
    window.removeEventListener('storage', this.storageEventListener.bind(this));
  }

  login(username: string, password: string, rememberMe: boolean) {
    const tokenRequest: TokenRequest = {
      email: username,
      password: password,
      rememberMe: rememberMe
    };
    return this.http
      .post<Result<TokenResponse>>(`${this.apiUrl}/api/identity/token`, tokenRequest)
      .pipe(
        map(
          (x: Result<TokenResponse>) => {
            if (x.succeeded) {
              this._user.next({
                name: x.data.name,
                email: x.data.email,
                refreshToken: x.data.refreshToken,
                refreshTokenExpiryTime: x.data.refreshTokenExpiryTime,
                roles: x.data.roles,
                token: x.data.token,
                userImageURL: x.data.userImageURL
              });
              this.rootScope.SetTokenUser({
                email: x.data.email,
                name: x.data.name,
                roles: x.data.roles,
                userPicture: x.data.userImageURL
              });
              this.setLocalStorage(x.data);
              this.startTokenTimer();
            }
            return x;
          }
        )
      );
  }

  logout() {
    this.clearLocalStorage();
    this._user.next(null);
    this.stopTokenTimer();
  }

  refreshToken(): Observable<Result<TokenResponse> | null> {
    const refreshToken = localStorage.getItem('refresh_token');
    const token = localStorage.getItem('access_token');
    if (!refreshToken) {
      this.clearLocalStorage();
      return of(null);
    }

    const request: RefreshTokenRequest = {
      refreshToken: refreshToken,
      token: token
    };

    return this.http
      .post<Result<TokenResponse>>(`${this.apiUrl}/api/identity/token/refresh`, request)
      .pipe(
        map((x: Result<TokenResponse>) => {
          this._user.next({
            name: x.data.name,
            email: x.data.email,
            refreshToken: x.data.refreshToken,
            refreshTokenExpiryTime: x.data.refreshTokenExpiryTime,
            roles: x.data.roles,
            token: x.data.token,
            userImageURL: x.data.userImageURL
          });
          this.rootScope.SetTokenUser({
            email: x.data.email,
            name: x.data.name,
            roles: x.data.roles,
            userPicture: x.data.userImageURL
          });
          this.setLocalStorage(x.data);
          this.startTokenTimer();
          return x;
        })
      );
  }

  setLocalStorage(x: TokenResponse) {
    localStorage.setItem('access_token', x.token);
    localStorage.setItem('refresh_token', x.refreshToken);
    localStorage.setItem('login-event', 'login' + Math.random());
  }

  clearLocalStorage() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.setItem('logout-event', 'logout' + Math.random());
  }

  private getTokenRemainingTime() {
    const accessToken = localStorage.getItem('access_token');
    if (!accessToken) {
      return 0;
    }
    const jwtToken = JSON.parse(atob(accessToken.split('.')[1]));
    const expires = new Date(jwtToken.exp * 1000);
    return expires.getTime() - Date.now();
  }

  private startTokenTimer() {
    const timeout = this.getTokenRemainingTime();
    this.timer = of(true)
      .pipe(
        delay(timeout),
        tap({
          next: () => this.refreshToken().subscribe(),
        })
      )
      .subscribe();
  }

  private stopTokenTimer() {
    this.timer?.unsubscribe();
  }
}
