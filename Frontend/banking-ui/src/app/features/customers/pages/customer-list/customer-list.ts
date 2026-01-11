import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { CustomerService } from '../../services/customer.service';
import { Customer } from '../../../../core/models/customers';
import { Loader } from '../../../../shared/components/loader/loader';
import { ConfirmDialog } from '../../../../shared/components/confirm-dialog/confirm-dialog';
import { NotificationService } from '../../../../core/services/notification';

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [CommonModule, RouterLink, Loader, ConfirmDialog],
  templateUrl: './customer-list.html',
  styleUrl: './customer-list.css',
})
export class CustomerList implements OnInit {
  private _customerService = inject(CustomerService);
  private _notificationService = inject(NotificationService);
  private cd = inject(ChangeDetectorRef);

  customers: Customer[] = [];
  isLoading = true;

  // Dialog State
  isDeleteDialogOpen = false;
  selectedCustomerId: number | null = null;

  ngOnInit(): void {
    this.loadCustomers();
  }

  // 1. Verileri Getir
  loadCustomers() {
    this.isLoading = true;
    
    this._customerService.getAll()
      .pipe(
        finalize(() => {
          this.isLoading = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: (res: any) => {
          console.log('Backend Müşteri Cevabı:', res);

          if (Array.isArray(res)) {
            this.customers = res;
          } else {
            this.customers = res?.data || [];
          }
        },
        error: (err) => {
          console.error('Müşteriler yüklenemedi', err);
          this._notificationService.error('Müşteri listesi yüklenemedi.');
        }
      });
  }

  // Silme Butonuna Tıklanınca
  onDeleteClick(id: number) {
    this.selectedCustomerId = id;
    this.isDeleteDialogOpen = true;
  }

  // 2. Silme İşlemi
  confirmDelete() {
    if (this.selectedCustomerId) {
      this.isLoading = true;
      this.isDeleteDialogOpen = false;

      this._customerService.delete(this.selectedCustomerId)
        .pipe(
          finalize(() => {
            this.isLoading = false;
            this.cd.detectChanges();
            this.selectedCustomerId = null;
          })
        )
        .subscribe({
          next: () => {
            // Listeden çıkar
            this.customers = this.customers.filter(c => c.id !== this.selectedCustomerId);
            
            // 5. Başarı Bildirimi
            this._notificationService.success('Müşteri başarıyla silindi. 🗑️');
          },
          error: (err) => {
            console.error('Silme hatası', err);
          }
        });
    }
  }

  cancelDelete() {
    this.isDeleteDialogOpen = false;
    this.selectedCustomerId = null;
  }
}