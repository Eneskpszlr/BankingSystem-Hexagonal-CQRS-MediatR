import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { Account } from '../../../../core/models/accounts';

// Shared Bileşenler
import { Loader } from '../../../../shared/components/loader/loader';
import { ConfirmDialog } from '../../../../shared/components/confirm-dialog/confirm-dialog'; 
import { StatusTrPipe } from '../../../../shared/pipes/status-tr-pipe';

@Component({
  selector: 'app-account-list',
  imports: [CommonModule, RouterLink, Loader, ConfirmDialog, Loader, StatusTrPipe],
  templateUrl: './account-list.html',
  styleUrl: './account-list.css',
})
export class AccountList {
  // Dependency Injection
  private _accountService = inject(AccountService);

  // State (Veri Durumu)
  accounts: Account[] = [];
  isLoading: boolean = false;

  // Dialog State (Silme Onayı İçin)
  isDeleteDialogOpen: boolean = false;
  selectedAccountId: number | null = null; // Hangi hesabın silineceği

  ngOnInit(): void {
    this.loadAccounts();
  }

  // 1. Verileri Getir
  loadAccounts() {
    this.isLoading = true;

    this._accountService.getAll().subscribe({
      next: (response) => {
        // Backend'den { success: true, data: [...] } geliyor
        this.accounts = response.data; 
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Hata oluştu:', err);
        this.isLoading = false;
        // İleride buraya Toastr (Hata mesajı) ekleyeceğiz
      }
    });
  }

  // 2. Sil Butonuna Basınca (Dialogu Aç)
  onDeleteClick(id: number) {
    this.selectedAccountId = id;
    this.isDeleteDialogOpen = true;
  }

  // 3. Dialogda "Evet"e Basınca (Silme İşlemi)
  confirmDelete() {
    if (this.selectedAccountId) {
      this.isLoading = true; // Yükleniyor aç
      this.isDeleteDialogOpen = false; // Dialogu kapat

      this._accountService.delete(this.selectedAccountId).subscribe({
        next: () => {
          // Listeden silineni frontend tarafında da çıkart (Tekrar istek atmaya gerek yok, performans!)
          this.accounts = this.accounts.filter(a => a.id !== this.selectedAccountId);
          this.isLoading = false;
          this.selectedAccountId = null;
        },
        error: (err) => {
          console.error('Silme hatası:', err);
          this.isLoading = false;
        }
      });
    }
  }

  // 4. Dialogda "Vazgeç"e Basınca
  cancelDelete() {
    this.isDeleteDialogOpen = false;
    this.selectedAccountId = null;
  }
}
