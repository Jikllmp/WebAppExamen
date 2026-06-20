import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthStateService } from '../services/auth-state';

export const agenceGuard: CanActivateFn = (route, state) => {
  const authState = inject(AuthStateService);
  const router = inject(Router);

  if (authState.token && authState.getRole() === 'Agence') {
    return true;
  }

  router.navigate(['/login']);
  return false;
};