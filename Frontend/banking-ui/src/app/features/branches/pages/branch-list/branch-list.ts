import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { BranchService } from '../../services/branch.service';
import { Branch } from '../../../../core/models/branches';
import { Loader } from '../../../../shared/components/loader/loader';
import { ConfirmDialog } from '../../../../shared/components/confirm-dialog/confirm-dialog';

@Component({
  selector: 'app-branch-list',
  imports: [CommonModule, RouterLink, Loader, ConfirmDialog],
  templateUrl: './branch-list.html',
  styleUrl: './branch-list.css',
})
export class BranchList implements OnInit {
  private _branchService = inject(BranchService);

  branches: Branch[] = [];
  isLoading = false;

  // Dialog State
  isDeleteDialogOpen = false;
  selectedBranchId: number | null = null;

  ngOnInit(): void {
    this.loadBranches();
  }

  loadBranches() {
    this.isLoading = true;
    this._branchService.getAll().subscribe({
      next: (res) => {
        this.branches = res.data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isLoading = false;
      }
    });
  }

  onDeleteClick(id: number) {
    this.selectedBranchId = id;
    this.isDeleteDialogOpen = true;
  }

  confirmDelete() {
    if (this.selectedBranchId) {
      this.isLoading = true;
      this.isDeleteDialogOpen = false;

      this._branchService.delete(this.selectedBranchId).subscribe({
        next: () => {
          this.branches = this.branches.filter(b => b.id !== this.selectedBranchId);
          this.isLoading = false;
          this.selectedBranchId = null;
        },
        error: (err) => {
          console.error(err);
          this.isLoading = false;
        }
      });
    }
  }

  cancelDelete() {
    this.isDeleteDialogOpen = false;
    this.selectedBranchId = null;
  }
}
