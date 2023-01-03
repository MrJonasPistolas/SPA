import {
  Component,
  OnInit
} from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';

import { EXMConfig } from '../../config';
import {
  LanguageViewer,
  RoleResponse,
  TokenUserResponse
} from '../../models';
import { RootScope } from '../../scopes';
import {
  AuthService,
  HelperService
} from '../../services';

declare var ThemeCustomizer: any;
declare var bootstrap: any;

@Component({
  selector: 'app-app-layout',
  templateUrl: './app-layout.component.html',
  styleUrls: ['./app-layout.component.css']
})
export class AppLayoutComponent implements OnInit {

  public selectedLanguage: string = '';
  public availableLanguages: Array<LanguageViewer> = new Array<LanguageViewer>();
  public user: TokenUserResponse | null = null;
  public userRoles: Array<RoleResponse> | undefined;
  public roleName: string = '';
  public modal: any;

  constructor(
    private rootScope: RootScope,
    private translateService: TranslateService,
    public helperService: HelperService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.selectedLanguage = this.rootScope.GetLanguage();

    this.translateService.get(['layout.topnavbar.languages.en', 'layout.topnavbar.languages.pt']).subscribe((a: any) => {
      environment.languageSupport.forEach((value: string) => {
        this.availableLanguages.push({
          code: value,
          title: a[`layout.topnavbar.languages.${value}`],
          selected: value.toLowerCase() === this.selectedLanguage.toLowerCase()
        });
      });
    });

    this.user = this.rootScope.GetTokenUser();
    this.userRoles = this.user?.roles;

    this.getRoleName();

    new EXMConfig().Init();
    new ThemeCustomizer().init();

    this.modal = new bootstrap.Modal(
      document.getElementById('modal-logout'),
      {
        keyboard: false,
        backdrop: 'static'
      }
    );
  }

  public getRoleName() {
    const role: string = this.userRoles && this.userRoles[0] && this.userRoles[0].roleName.toString() || '';
    this.translateService.get(`layout.topnavbar.user.${role.toLowerCase()}`).subscribe((value: string) => {
      this.roleName = value;
    });
  }

  public showLogout() {
    this.modal.show();
  }

  public logout() {
    this.authService.logout();
    document.location.reload();
  }

}
