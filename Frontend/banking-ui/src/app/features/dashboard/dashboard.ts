import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core'; // 1. IMPORT EKLE
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { finalize, forkJoin } from 'rxjs';
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
export class Dashboard implements OnInit {
  private _accountService = inject(AccountService);
  private _customerService = inject(CustomerService);
  private _branchService = inject(BranchService);
  
  // 2. INJECT EDİYORUZ (Bunu unutmuşsun)
  private cd = inject(ChangeDetectorRef); 

  isLoading = true;

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
    this.isLoading = true;

    forkJoin({
      accounts: this._accountService.getAll(),
      customers: this._customerService.getAll(),
      branches: this._branchService.getAll()
    })
    .pipe(
      finalize(() => {
        this.isLoading = false;
        this.cd.detectChanges(); // Artık burası hata vermez ✅
      })
    )
    .subscribe({
      next: (res: any) => {
        console.log('Veriler Geldi:', res);

        // 1. Müşteri Sayısı
        const customerList = res.customers?.data || res.customers || [];
        this.stats.totalCustomers = customerList.length || 0;

        // 2. Şube Sayısı
        const branchList = res.branches?.data || res.branches || [];
        this.stats.totalBranches = branchList.length || 0;

        // 3. Hesap Sayısı
        const accounts = res.accounts?.data || res.accounts || [];
        this.stats.totalAccounts = accounts.length || 0;

        // Bakiye Hesaplama
        this.stats.totalBalanceTRY = Array.isArray(accounts) 
          ? accounts
              .filter((acc: any) => acc.currencyCode === 'TRY')
              .reduce((sum: number, acc: any) => sum + (acc.balance || 0), 0)
          : 0;
      },
      error: (err) => {
        console.error('Hata oluştu:', err);
      }
    });
  }
}