import { Injectable } from "@angular/core";

import { Role } from "../enums";

import {
  LanguageViewer,
  RoleResponse
} from "../models";
import { RootScope } from "../scopes";

@Injectable({
  providedIn: 'root'
})
export class HelperService {

  // Constructor
  constructor(private rootScope: RootScope) { }

  public FilterSelectedLanguage(language: LanguageViewer) {
    return language.selected;
  }

  public FilterNotSelectedLanguage(language: LanguageViewer) {
    return !language.selected;
  }

  public FilterSelectedRole(role: RoleResponse) {
    return role.selected;
  }

  public IsAdmin(): boolean {
    const user = this.rootScope.GetTokenUser();
    return user?.roles[0].roleName === Role.Administrator;
  }
}
