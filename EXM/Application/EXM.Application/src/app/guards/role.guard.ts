import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot
} from "@angular/router";

import { RootScope } from "../scopes";

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(
    private router: Router,
    private rootScope: RootScope
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const user = this.rootScope.GetTokenUser();
    // Check if route is restricted by role
    if (route.data['roles'] && route.data['roles'].indexOf(user?.roles[0].roleName) === -1) {
      this.router.navigate(['/']);
      return false;
    }

    return true;
  }
}
