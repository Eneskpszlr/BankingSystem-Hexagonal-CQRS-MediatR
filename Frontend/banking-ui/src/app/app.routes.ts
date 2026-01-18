import { Routes } from '@angular/router';
import { MainLayout } from './layout/main-layout/main-layout';

export const routes: Routes = [
  {
    // 1. ANA KAPSAYICI (Layout)
    path: '',
    component: MainLayout, 
    children: [
      // 2. CHİLD ROTALAR (Navbar'ın altında değişecek kısımlar)
      { 
        path: '', 
        loadComponent: () => import('./features/dashboard/dashboard').then(m => m.Dashboard) 
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
        loadComponent: () => import('./features/customers/pages/customer-list/customer-list').then(m => m.CustomerList)
      },
      { 
        path: 'customers/create', 
        loadComponent: () => import('./features/customers/pages/create-customer/create-customer').then(m => m.CreateCustomer)
      },
      { 
        path: 'customers/update/:id', 
        loadComponent: () => import('./features/customers/pages/update-customer/update-customer').then(m => m.UpdateCustomer)
      },

      // --- BRANCHES ---
      { 
        path: 'branches', 
        loadComponent: () => import('./features/branches/pages/branch-list/branch-list').then(m => m.BranchList)
      },
      { 
        path: 'branches/create', 
        loadComponent: () => import('./features/branches/pages/create-branch/create-branch').then(m => m.CreateBranch)
      },
      { 
        path: 'branches/update/:id', 
        loadComponent: () => import('./features/branches/pages/update-branch/update-branch').then(m => m.UpdateBranch)
      }
    ]
  },
];