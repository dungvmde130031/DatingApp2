import { inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastrService = inject(ToastrService);
  
  return accountService.currentUser$.pipe(
    map(user => {
      if (!user) return false;

      if (user.roles.includes('Admin') || user.roles.includes('Moderator')) {
        return true;
      } else {
        toastrService.error('You cannot enter this area!');
        return false;
      }
    }
  ))
  
}
