import { Injectable } from "@angular/core";

import {
  LanguageViewer,
  RoleResponse
} from "../models";

@Injectable({
  providedIn: 'root'
})
export class HelperService {

  // Constructor
  constructor() { }

  public FilterSelectedLanguage(language: LanguageViewer) {
    return language.selected;
  }

  public FilterNotSelectedLanguage(language: LanguageViewer) {
    return !language.selected;
  }

  public FilterSelectedRole(role: RoleResponse) {
    return role.selected;
  }
}
