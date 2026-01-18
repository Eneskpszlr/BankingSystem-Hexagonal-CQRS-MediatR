import { Injectable, signal } from '@angular/core';

export type ToastType = 'success' | 'error' | 'info' | 'warning';

export interface Toast {
  id: number;
  message: string;
  type: ToastType;
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  toasts = signal<Toast[]>([]);

  // 1. Başarı Mesajı
  success(message: string) {
    this.add(message, 'success');
  }

  // 2. Hata Mesajı
  error(message: string) {
    this.add(message, 'error');
  }

  // 3. Bilgi Mesajı
  info(message: string) {
    this.add(message, 'info');
  }

  // 4. Uyarı Mesajı
  warning(message: string) {
    this.add(message, 'warning');
  }

  // Toast Ekleme
  private add(message: string, type: ToastType) {
    const id = Date.now();
    const newToast: Toast = { id, message, type };

    // Listeye ekle
    this.toasts.update(current => [...current, newToast]);

    setTimeout(() => {
      this.remove(id);
    }, 3000);
  }

  // Toast Silme (Manuel kapatma veya süre dolunca)
  remove(id: number) {
    this.toasts.update(current => current.filter(t => t.id !== id));
  }
}