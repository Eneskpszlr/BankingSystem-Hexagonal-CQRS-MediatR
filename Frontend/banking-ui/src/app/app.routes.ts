import { Routes } from '@angular/router';
import { MainLayout } from './layout/main-layout/main-layout';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
  
  // 1. AUTH ROTALARI (Layout DIŞINDA - Navbar GÖRÜNMEZ)
  // Bu sayfalara herkes erişebilir.
  {
    path: 'auth',
    children: [
      { 
        path: 'login', 
        loadComponent: () => import('./features/auth/login/login').then(m => m.Login) 
      },
      { 
        path: 'register', 
        loadComponent: () => import('./features/auth/register/register').then(m => m.Register) 
      }
    ]
  },

  // 2. UYGULAMA ROTALARI (Layout İÇİNDE - Navbar GÖRÜNÜR)
  //  canActivate: [authGuard] -> Bu satır tüm alt sayfaları kilitler.
  // Giriş yapmamış kişi buradaki hiçbir sayfayı göremez.
  {
    path: '',
    component: MainLayout, 
    canActivate: [authGuard],
    children: [
      { 
        path: '', 
        loadComponent: () => import('./features/dashboard/dashboard').then(m => m.Dashboard) 
      },
      { 
        path: 'profile', 
        loadComponent: () => import('./features/profile/profile').then(m => m.Profile) 
      },
      
      // --- ACCOUNTS ---
      { 
        path: 'accounts', 
        loadComponent: () => import('./features/accounts/pages/account-list/account-list').then(m => m.AccountList)
      },
      { 
        path: 'accounts/create', 
        loadComponent: () => import('./features/accounts/pages/create-account/create-account').then(m => m.CreateAccount)
      },
      { 
        path: 'accounts/update/:id', 
        loadComponent: () => import('./features/accounts/pages/update-account/update-account').then(m => m.UpdateAccount)
      },

      // --- TRANSACTIONS ---
      { 
        path: 'transactions', 
        loadComponent: () => import('./features/transactions/pages/transaction-history/transaction-history').then(m => m.TransactionHistory)
      },
      { 
        path: 'transactions/create', 
        loadComponent: () => import('./features/transactions/pages/create-transaction/create-transaction').then(m => m.CreateTransaction)
      },

      // --- CUSTOMERS ---
      { 
        path: 'customers', 
        canActivate: [roleGuard],
        loadComponent: () => import('./features/customers/pages/customer-list/customer-list').then(m => m.CustomerList)
      },
      { 
        path: 'customers/create', 
        canActivate: [roleGuard],
        loadComponent: () => import('./features/customers/pages/create-customer/create-customer').then(m => m.CreateCustomer)
      },
      { 
        path: 'customers/update/:id', 
        canActivate: [roleGuard],
        loadComponent: () => import('./features/customers/pages/update-customer/update-customer').then(m => m.UpdateCustomer)
      },

      // --- BRANCHES ---
      { 
        path: 'branches', 
        canActivate: [roleGuard],
        loadComponent: () => import('./features/branches/pages/branch-list/branch-list').then(m => m.BranchList)
      },
      { 
        path: 'branches/create', 
        canActivate: [roleGuard],
        loadComponent: () => import('./features/branches/pages/create-branch/create-branch').then(m => m.CreateBranch)
      },
      { 
        path: 'branches/update/:id', 
        canActivate: [roleGuard],
        loadComponent: () => import('./features/branches/pages/update-branch/update-branch').then(m => m.UpdateBranch)
      }
    ]
  },

  // 3. Yanlış URL girilirse ana sayfaya (veya login'e) at
  { path: '**', redirectTo: '' }
];