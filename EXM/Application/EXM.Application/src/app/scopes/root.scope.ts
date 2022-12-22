export class RootScope {
  // Properties
  private _Language: string | undefined;

  // Methods

  
  GetLanguage(): string {
    return this._Language !== undefined ? this._Language : '';
  }

  SetLanguage(language: string) {
    this._Language = language;
  }
}
