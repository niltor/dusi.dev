import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard  {
  constructor(
    private router: Router,
    private auth: LoginService,
  ) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean | UrlTree {
    const url = state.url;

    const publicRoutes = ['/blog', '/index', '/news', '/system/login','/video-preview'];

    if (this.auth.isAdmin) {
      return true;
    } else {

      for (const route of publicRoutes) {
        if (url.startsWith(route)) {
          return true;
        }
      }

      if (url.startsWith('/system')) {
        return false;
      }

      if (this.auth.isLogin) {
        return true;
      }
      return this.router.parseUrl('/index');
    }
  }
  canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean | UrlTree {
    return this.canActivate(next, state);
  }
}
