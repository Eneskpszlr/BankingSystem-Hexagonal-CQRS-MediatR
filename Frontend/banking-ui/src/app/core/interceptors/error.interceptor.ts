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
        // 1. Client-side hatası (Tarayıcı/Ağ kaynaklı)
        errorMessage = `Ağ Hatası: ${error.error.message}`;
      } else {
        // 2. Server-side hatası
        switch (error.status) {
          
          case 400:
            // Backend Validation hatalarını yakala
            if (error.error?.errors) {
              const validationErrors = Object.values(error.error.errors).flat();
              errorMessage = validationErrors.join(', ');
            } else {
              errorMessage = error.error?.title || 'Geçersiz İstek (400)';
            }
            break;

          case 401: 
            errorMessage = 'Oturum süreniz doldu. Lütfen tekrar giriş yapın. (401)'; 
            break;
            
          case 403: 
            errorMessage = 'Bu işlem için yetkiniz yok. (403)'; 
            break;
            
          case 404: 
            errorMessage = 'İstenilen veri veya kaynak bulunamadı. (404)'; 
            break;
            
          case 500: 
            errorMessage = 'Sunucu tarafında bir hata oluştu. Lütfen daha sonra tekrar deneyin. (500)'; 
            break;

          case 0:
            errorMessage = 'Sunucuya ulaşılamıyor. Bağlantınızı kontrol edin.';
            break;

          default: 
            errorMessage = `Beklenmeyen Hata: ${error.message}`;
        }
      }

      // 3. Bildirimi göster
      notificationService.error(errorMessage);

      return throwError(() => error);
    })
  );
};