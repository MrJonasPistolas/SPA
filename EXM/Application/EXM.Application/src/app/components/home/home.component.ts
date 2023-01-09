import { Component } from '@angular/core';
import { EmitActionsConstants } from '../../constants';
import { MainLayoutEmit } from '../../emits';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(
    private mainLayoutEmit: MainLayoutEmit
  ) {
    this.mainLayoutEmit.emitChange({
      Action: EmitActionsConstants.BreadcrumbChanged,
      Data: [
        {
          href: '',
          title: 'Home',
          isActive: true
        }
      ]
    });
  }

}
