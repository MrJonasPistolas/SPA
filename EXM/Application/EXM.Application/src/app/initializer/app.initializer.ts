import { HttpClient } from "@angular/common/http";
import { TranslateService } from "@ngx-translate/core";
import {
  catchError,
  forkJoin,
  of
} from "rxjs";
import { RootScope } from "../scopes";
import { AuthService } from "../services";

export function AppInitializer(
  authService: AuthService,
  httpClient: HttpClient,
  translateService: TranslateService,
  rootScope: RootScope
) {

  return () => new Promise<boolean>(
    (resolve: (res: boolean) => void) => {
      const defaultLocale = 'pt';
      const translationsUrl = '/assets/i18n';
      const suffix = '.json';
      const storageLocale = localStorage.getItem('locale');
      const locale = storageLocale || defaultLocale;

      forkJoin([
        httpClient.get(`${translationsUrl}/${locale}${suffix}`).pipe(
          catchError(() => of(null))
        ),
        authService.refreshToken()
      ]).subscribe(
        (response: any[]) => {
          const translatedKeys = response[0];
          const refreshedToken = response[1];

          translateService.setTranslation(locale, translatedKeys || {}, true);
          rootScope.SetTranslations(translatedKeys);

          translateService.setDefaultLang(defaultLocale);
          translateService.use(locale);

          rootScope.SetLanguage(locale);

          resolve(true);
        }
      );
    }
  );
}
