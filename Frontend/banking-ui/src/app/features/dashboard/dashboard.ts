import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { finalize, forkJoin } from 'rxjs';
import { AccountService } from '../accounts/services/account.service';
import { CustomerService } from '../customers/services/customer.service';
import { BranchService } from '../branches/services/branch.service';
import { AuthService } from '../../core/services/auth.service';
import { Loader } from '../../shared/components/loader/loader';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, Loader],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  private _accountService = inject(AccountService);
  private _customerService = inject(CustomerService);
  private _branchService = inject(BranchService);
  private _authService = inject(AuthService);
  private cd = inject(ChangeDetectorRef);

  // Kullanıcı Bilgisi
  user = this._authService.currentUser;
  isAdmin = this._authService.isAdmin;

  isLoading = true;

  // Admin İstatistikleri
  adminStats = {
    totalCustomers: 0,
    totalAccounts: 0,
    totalBranches: 0,
    totalBalanceTRY: 0
  };

  // Müşteri İstatistikleri
  customerStats = {
    myAccounts: [] as any[],
    totalBalance: 0
  };

  ngOnInit(): void {
    if (this.isAdmin()) {
      this.loadAdminData();
    } else {
      this.loadCustomerData();
    }
  }

  // 1. ADMIN VERİLERİ (Genel Bakış)
  loadAdminData() {
    this.isLoading = true;
    forkJoin({
      accounts: this._accountService.getAll(),
      customers: this._customerService.getAll(),
      branches: this._branchService.getAll()
    })
    .pipe(
      finalize(() => { 
        this.isLoading = false; 
        this.cd.detectChanges(); 
      })
    )
    .subscribe({
      next: (res: any) => {
        const accounts = Array.isArray(res.accounts) ? res.accounts : (res.accounts?.data || []);
        const customers = Array.isArray(res.customers) ? res.customers : (res.customers?.data || []);
        const branches = Array.isArray(res.branches) ? res.branches : (res.branches?.data || []);

        this.adminStats.totalCustomers = customers.length;
        this.adminStats.totalBranches = branches.length;
        this.adminStats.totalAccounts = accounts.length;

        // Sadece TRY hesaplarını topla
        this.adminStats.totalBalanceTRY = accounts
          .filter((acc: any) => acc.currencyCode === 'TRY')
          .reduce((sum: number, acc: any) => sum + (acc.balance || 0), 0);
      },
      error: (err) => {
        console.error('Admin verileri yüklenirken hata:', err);
      }
    });
  }

  // 2. MÜŞTERİ VERİLERİ (Sadece Benim Hesaplarım)
  loadCustomerData() {
    const userId = this.user()?.id;
    
    // Eğer ID yoksa işlemi durdur (Güvenlik)
    if (!userId) {
      this.isLoading = false;
      return;
    }

    this.isLoading = true;
    
    this._accountService.getByCustomerId(userId)
      .pipe(
        finalize(() => { 
          this.isLoading = false; 
          this.cd.detectChanges(); 
        })
      )
      .subscribe({
        next: (res: any) => {
          // Başarılı gelirse listeyi al
          const myAccounts = Array.isArray(res) ? res : (res.data || []);
          this.customerStats.myAccounts = myAccounts;

          // Toplam Varlık Hesapla (Sadece TRY)
          this.customerStats.totalBalance = myAccounts
            .filter((acc: any) => acc.currencyCode === 'TRY')
            .reduce((sum: number, acc: any) => sum + (acc.balance || 0), 0);
        },
        error: (err) => {
          if (err.status === 404) {
            console.log('Kullanıcının henüz hesabı yok (404 Normal Durum).');
            this.customerStats.myAccounts = [];
            this.customerStats.totalBalance = 0;
          } else {
            console.error('Müşteri verileri yüklenirken beklenmedik hata:', err);
          }
        }
      });
  }
}