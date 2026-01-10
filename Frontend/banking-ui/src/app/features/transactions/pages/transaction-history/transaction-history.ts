import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TransactionService } from '../../services/transaction.service';
import { AccountService } from '../../../accounts/services/account.service';
import { Transaction } from '../../../../core/models/transactions';
import { Account } from '../../../../core/models/accounts';
import { Loader } from '../../../../shared/components/loader/loader';
import { StatusTrPipe } from '../../../../shared/pipes/status-tr-pipe';

@Component({
  selector: 'app-transaction-history',
  imports: [CommonModule, FormsModule, Loader, StatusTrPipe],
  templateUrl: './transaction-history.html',
  styleUrl: './transaction-history.css',
})
export class TransactionHistory implements OnInit {
  private _transactionService = inject(TransactionService);
  private _accountService = inject(AccountService);

  accounts: Account[] = [];
  transactions: Transaction[] = [];
  
  selectedAccountId: number = 0;
  isLoading: boolean = false;

  ngOnInit(): void {
    this.loadAccounts();
  }

  // 1. Önce Hesapları Getir
  loadAccounts() {
    this.isLoading = true;
    this._accountService.getAll().subscribe({
      next: (res) => {
        this.accounts = res.data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Hesaplar yüklenemedi', err);
        this.isLoading = false;
      }
    });
  }

  // 2. Dropdown değişince çalışır
  onAccountChange() {
    if (this.selectedAccountId > 0) {
      this.loadTransactions(this.selectedAccountId);
    } else {
      this.transactions = []; // "Seçiniz"e dönerse tabloyu boşalt
    }
  }

  // 3. Seçilen Hesabın Hareketlerini Getir
  loadTransactions(accountId: number) {
    this.isLoading = true;
    this._transactionService.getByAccountId(accountId).subscribe({
      next: (res) => {
        // Backend işlemlerin tarihine göre sıralı göndermiyorsa burada sort edebilirsin
        // res.data.sort((a, b) => new Date(b.createdDate).getTime() - new Date(a.createdDate).getTime());
        this.transactions = res.data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('İşlemler yüklenemedi', err);
        this.isLoading = false;
      }
    });
  }
}
