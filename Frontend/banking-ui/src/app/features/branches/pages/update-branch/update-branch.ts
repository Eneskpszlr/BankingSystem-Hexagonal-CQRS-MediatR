import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { BranchService } from '../../services/branch.service';
import { updateBranchForm, toUpdateBranchRequest } from '../../validations/update-branch.form';
import { Loader } from '../../../../shared/components/loader/loader';

@Component({
  selector: 'app-update-branch',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './update-branch.html',
  styleUrl: './update-branch.css',
})
export class UpdateBranch implements OnInit {
  private _branchService = inject(BranchService);
  private _route = inject(ActivatedRoute);
  private _router = inject(Router);

  form = updateBranchForm();
  isLoading = true;
  isSubmitting = false;

  ngOnInit(): void {
    const id = this._route.snapshot.paramMap.get('id');
    if (id) {
      this.loadBranch(Number(id));
    }
  }

  loadBranch(id: number) {
    this._branchService.getById(id).subscribe({
      next: (res) => {
        this.form.patchValue(res.data);
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isLoading = false;
        this._router.navigate(['/branches']);
      }
    });
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    const request = toUpdateBranchRequest(this.form);

    this._branchService.update(request).subscribe({
      next: () => {
        this.isSubmitting = false;
        this._router.navigate(['/branches']);
      },
      error: (err) => {
        console.error(err);
        this.isSubmitting = false;
      }
    });
  }
}
