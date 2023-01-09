import {
  Component,
  OnInit
} from '@angular/core';

import { EXMConfig } from '../../config';
import { EmitActionsConstants } from '../../constants';
import { MainLayoutEmit } from '../../emits';
import {
  ActionDataModel,
  BreadcrumbLayoutViewer,
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
  public breadcrumb: Array<BreadcrumbLayoutViewer> = new Array<BreadcrumbLayoutViewer>();
  public pageTitle: string = '';
  private translations: any = {};

  constructor(
    private mainLayoutEmit: MainLayoutEmit,
    private rootScope: RootScope,
    public helperService: HelperService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.breadcrumb = [];
    this.selectedLanguage = this.rootScope.GetLanguage();
    this.translations = this.rootScope.GetTranslations();

    this.availableLanguages.push(
      {
        code: 'en',
        title: this.translations.layout.topnavbar.languages.en,
        selected: 'en' === this.selectedLanguage.toLowerCase()
      },
      {
        code: 'pt',
        title: this.translations.layout.topnavbar.languages.pt,
        selected: 'pt' === this.selectedLanguage.toLowerCase()
      }
    );

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

    this.mainLayoutEmit.ChangeEmitted.subscribe(
      (result: ActionDataModel) => {
        switch (result.Action) {
          case EmitActionsConstants.BreadcrumbChanged:
            this.breadcrumb = result.Data;
            break;
          case EmitActionsConstants.PageTitleChanged:
            this.pageTitle = result.Data;
            break;
        }
      }
    );
  }

  public getRoleName() {
    const role: string = this.userRoles && this.userRoles[0] && this.userRoles[0].roleName.toString() || '';
    this.roleName = this.translations.layout.topnavbar.user[role.toLowerCase()];
  }

  public changeLanguage(code: string) {
    this.rootScope.SetLanguage(code);
    localStorage.setItem('locale', code);
    window.location.reload();
  }

  public showLogout() {
    this.modal.show();
  }

  public logout() {
    this.authService.logout();
    document.location.reload();
  }

}
