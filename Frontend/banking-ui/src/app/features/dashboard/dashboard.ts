import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';
import { AccountService } from '../accounts/services/account.service';
import { CustomerService } from '../customers/services/customer.service';
import { BranchService } from '../branches/services/branch.service';
import { Loader } from '../../shared/components/loader/loader';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, RouterLink, Loader],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
  private _accountService = inject(AccountService);
  private _customerService = inject(CustomerService);
  private _branchService = inject(BranchService);

  isLoading = true;

  // İstatistik Verileri
  stats = {
    totalCustomers: 0,
    totalAccounts: 0,
    totalBranches: 0,
    totalBalanceTRY: 0
  };

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData() {
    forkJoin({
      accounts: this._accountService.getAll(),
      customers: this._customerService.getAll(),
      branches: this._branchService.getAll()
    }).subscribe({
      next: (res) => {
        // 1. Müşteri Sayısı
        this.stats.totalCustomers = res.customers.data.length;

        // 2. Şube Sayısı
        this.stats.totalBranches = res.branches.data.length;

        // 3. Hesap Sayısı ve Bakiye Hesabı
        const accounts = res.accounts.data;
        this.stats.totalAccounts = accounts.length;

        // Sadece TRY olan hesapların bakiyesini topla
        this.stats.totalBalanceTRY = accounts
          .filter(acc => acc.currencyCode === 'TRY')
          .reduce((sum, acc) => sum + acc.balance, 0);

        this.isLoading = false;
      },
      error: (err) => {
        console.error('Dashboard verisi yüklenemedi', err);
        this.isLoading = false;
      }
    });
  }
}
