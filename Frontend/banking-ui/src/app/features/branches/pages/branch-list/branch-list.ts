import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { BranchService } from '../../services/branch.service';
import { Branch } from '../../../../core/models/branches';
import { Loader } from '../../../../shared/components/loader/loader';
import { ConfirmDialog } from '../../../../shared/components/confirm-dialog/confirm-dialog';

@Component({
  selector: 'app-branch-list',
  standalone: true,
  imports: [CommonModule, RouterLink, Loader, ConfirmDialog],
  templateUrl: './branch-list.html',
  styleUrl: './branch-list.css',
})
export class BranchList implements OnInit {
  private _branchService = inject(BranchService);
  private cd = inject(ChangeDetectorRef);

  branches: Branch[] = [];
  isLoading = true;

  // Dialog State
  isDeleteDialogOpen = false;
  selectedBranchId: number | null = null;

  ngOnInit(): void {
    this.loadBranches();
  }

  // 1. Verileri Getir
  loadBranches() {
    this.isLoading = true;

    this._branchService.getAll()
      .pipe(
        finalize(() => {
          this.isLoading = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: (res: any) => {
          
          console.log('Backendden Gelen TÜM CEVAP:', res); 

          if (Array.isArray(res)) {
             this.branches = res;
          } 
          else if (res && res.data) {
             this.branches = res.data;
          } 
          else {
             this.branches = [];
          }
        },
        error: (err) => {
          console.error('Şubeler yüklenirken hata:', err);
        }
      });
  }

  onDeleteClick(id: number) {
    this.selectedBranchId = id;
    this.isDeleteDialogOpen = true;
  }

  // 2. Silme İşlemi
  confirmDelete() {
    if (this.selectedBranchId) {
      this.isLoading = true;
      this.isDeleteDialogOpen = false;

      this._branchService.delete(this.selectedBranchId)
        .pipe(
          finalize(() => {
            this.isLoading = false;
            this.cd.detectChanges();
            this.selectedBranchId = null;
          })
        )
        .subscribe({
          next: () => {
            this.branches = this.branches.filter(b => b.id !== this.selectedBranchId);
          },
          error: (err) => {
            console.error('Silme hatası:', err);
          }
        });
    }
  }

  cancelDelete() {
    this.isDeleteDialogOpen = false;
    this.selectedBranchId = null;
  }
}