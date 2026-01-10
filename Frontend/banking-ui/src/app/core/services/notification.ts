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

  show(message: string, type: ToastType = 'info') {
    const id = Date.now();
    const newToast: Toast = { id, message, type };

    // Listeye ekle
    this.toasts.update(list => [...list, newToast]);

    // 3 saniye sonra otomatik sil
    setTimeout(() => {
      this.remove(id);
    }, 3000);
  }

  remove(id: number) {
    this.toasts.update(list => list.filter(t => t.id !== id));
  }
}