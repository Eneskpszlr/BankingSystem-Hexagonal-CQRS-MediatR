import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { NotificationService } from '../services/notification';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const notificationService = inject(NotificationService);

  if (authService.isLoggedIn()) {
    return true; // Geçiş serbest
  }

  // Giriş yapmamışsa:
  notificationService.warning('Bu sayfayı görüntülemek için giriş yapmalısınız.');
  router.navigate(['/auth/login']);
  return false;
};