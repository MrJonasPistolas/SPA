import { TokenUserResponse } from "../models";

export class RootScope {
  // Properties
  private _Language: string | undefined;
  private _TokenUser: TokenUserResponse | null = null;
  private _Translations = {};

  // Methods
  GetLanguage(): string {
    return this._Language !== undefined ? this._Language : '';
  }

  SetLanguage(language: string) {
    this._Language = language;
  }

  GetTokenUser(): TokenUserResponse | null {
    return this._TokenUser;
  }

  SetTokenUser(tokenUser: TokenUserResponse): void {
    this._TokenUser = tokenUser;
  }

  GetTranslations(): any {
    return this._Translations;
  }

  SetTranslations(translations: any): void {
    this._Translations = translations;
  }
}
