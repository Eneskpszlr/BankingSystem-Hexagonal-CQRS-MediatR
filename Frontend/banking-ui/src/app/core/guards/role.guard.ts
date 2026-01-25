import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { NotificationService } from '../services/notification';

export const roleGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const notificationService = inject(NotificationService);

  // Eğer kullanıcı Admin ise geçiş serbest
  if (authService.isAdmin()) {
    return true;
  }

  // Admin değilse (Müşteri ise) ve Admin sayfasına girmeye çalışıyorsa:
  notificationService.error('Bu sayfaya erişim yetkiniz yok! ⛔');
  router.navigate(['/']); // Dashboard'a geri postala
  return false;
};