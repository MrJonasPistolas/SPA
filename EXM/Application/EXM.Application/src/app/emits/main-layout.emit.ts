import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

import { ActionDataModel } from "../models";

@Injectable()
export class MainLayoutEmit {
  private EmitChangeSource = new Subject<ActionDataModel>();
  public ChangeEmitted = this.EmitChangeSource.asObservable();

  public emitChange(change: ActionDataModel): void {
    this.EmitChangeSource.next(change);
  }
}
