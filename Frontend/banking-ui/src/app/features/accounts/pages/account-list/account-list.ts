import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { AccountService } from '../../services/account.service';
import { Account } from '../../../../core/models/accounts';
import { Loader } from '../../../../shared/components/loader/loader';
import { ConfirmDialog } from '../../../../shared/components/confirm-dialog/confirm-dialog'; 
import { StatusTrPipe } from '../../../../shared/pipes/status-tr-pipe';

@Component({
  selector: 'app-account-list',
  imports: [CommonModule, RouterLink, Loader, ConfirmDialog, StatusTrPipe],
  templateUrl: './account-list.html',
  styleUrl: './account-list.css',
})
export class AccountList implements OnInit {
  // Dependency Injection
  private _accountService = inject(AccountService);
  private cd = inject(ChangeDetectorRef);

  // State (Veri Durumu)
  accounts: Account[] = [];
  isLoading: boolean = true;

  // Dialog State (Silme Onayı İçin)
  isDeleteDialogOpen: boolean = false;
  selectedAccountId: number | null = null; 

  ngOnInit(): void {
    this.loadAccounts();
  }

  // 1. Verileri Getir
  loadAccounts() {
    this.isLoading = true;

    this._accountService.getAll()
      .pipe(
        finalize(() => {
          this.isLoading = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: (response:any) => {
          console.log('Backendden Gelen TÜM CEVAP:', response); 
          if (Array.isArray(response)) {
             this.accounts = response;
          } 
          else if (response && response.data) {
             this.accounts = response.data;
          } 
          else {
             this.accounts = [];
          }
        },
        error: (err) => {
          console.error('Hesaplar yüklenirken hata:', err);
        }
      });
  }

  // 2. Sil Butonuna Basınca (Dialogu Aç)
  onDeleteClick(id: number) {
    this.selectedAccountId = id;
    this.isDeleteDialogOpen = true;
  }

  // 3. Dialogda "Evet"e Basınca
  confirmDelete() {
    if (this.selectedAccountId) {
      this.isLoading = true; 
      this.isDeleteDialogOpen = false;

      this._accountService.delete(this.selectedAccountId)
        .pipe(
          finalize(() => {
            this.isLoading = false;
            this.cd.detectChanges();
            this.selectedAccountId = null;
          })
        )
        .subscribe({
          next: () => {
            this.accounts = this.accounts.filter(a => a.id !== this.selectedAccountId);
          },
          error: (err) => {
            console.error('Silme hatası:', err);
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