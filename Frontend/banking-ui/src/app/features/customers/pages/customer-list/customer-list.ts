import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { CustomerService } from '../../services/customer.service';
import { Customer } from '../../../../core/models/customers';
import { Loader } from '../../../../shared/components/loader/loader';
import { ConfirmDialog } from '../../../../shared/components/confirm-dialog/confirm-dialog';

@Component({
  selector: 'app-customer-list',
  imports: [CommonModule, RouterLink, Loader, ConfirmDialog],
  templateUrl: './customer-list.html',
  styleUrl: './customer-list.css',
})
export class CustomerList {
  private _customerService = inject(CustomerService);

  // State
  customers: Customer[] = [];
  isLoading = false;

  // Dialog State
  isDeleteDialogOpen = false;
  selectedCustomerId: number | null = null;

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers() {
    this.isLoading = true;
    this._customerService.getAll().subscribe({
      next: (res) => {
        this.customers = res.data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Müşteriler yüklenemedi', err);
        this.isLoading = false;
      }
    });
  }

  // Silme Butonuna Tıklanınca
  onDeleteClick(id: number) {
    this.selectedCustomerId = id;
    this.isDeleteDialogOpen = true;
  }

  // Dialog Onaylayınca
  confirmDelete() {
    if (this.selectedCustomerId) {
      this.isLoading = true;
      this.isDeleteDialogOpen = false;

      this._customerService.delete(this.selectedCustomerId).subscribe({
        next: () => {
          // Listeden sil
          this.customers = this.customers.filter(c => c.id !== this.selectedCustomerId);
          this.isLoading = false;
          this.selectedCustomerId = null;
        },
        error: (err) => {
          console.error('Silme hatası', err);
          this.isLoading = false;
        }
      });
    }
  }

  cancelDelete() {
    this.isDeleteDialogOpen = false;
    this.selectedCustomerId = null;
  }
}
