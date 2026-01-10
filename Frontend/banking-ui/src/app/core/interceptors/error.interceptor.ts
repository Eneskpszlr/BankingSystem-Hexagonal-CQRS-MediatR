import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { NotificationService } from '../services/notification';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const notificationService = inject(NotificationService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = 'Bir hata oluştu!';

      if (error.error instanceof ErrorEvent) {
        // Client-side hatası
        errorMessage = `Hata: ${error.error.message}`;
      } else {
        // Server-side hatası
        switch (error.status) {
          case 400: errorMessage = 'Geçersiz İstek (400)'; break;
          case 401: errorMessage = 'Yetkisiz Giriş (401)'; break;
          case 404: errorMessage = 'Veri Bulunamadı (404)'; break;
          case 500: errorMessage = 'Sunucu Hatası (500)'; break;
          default: errorMessage = `Beklenmeyen Hata: ${error.message}`;
        }
      }

      // Bildirimi göster
      notificationService.show(errorMessage, 'error');

      return throwError(() => error);
    })
  );
};