// Core imports
import { Component } from '@angular/core';

// 3rd Party imports
import { TranslateService } from '@ngx-translate/core';

// App imports
import { environment } from '../../../environments/environment';
import { RootScope } from '../../scopes';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(
    private translate: TranslateService,
    private _RootScope: RootScope
  ) {
    var navigatorLanguage = navigator.languages[0].toLowerCase().split('-')[0];
    var isLanguage = environment.languageSupport.filter((element: string) => {
      return element.toLowerCase() === navigatorLanguage;
    });

    if (isLanguage.length > 0) {
      this.translate.setDefaultLang(navigatorLanguage);
      this._RootScope.SetLanguage(navigatorLanguage);
    } else {
      this.translate.setDefaultLang('en');
    }

    this.translate.setDefaultLang(
      isLanguage.length > 0 ? navigatorLanguage : 'en'
    );
    this._RootScope.SetLanguage(
      isLanguage.length > 0 ? navigatorLanguage : 'en'
    );
  }
}
