import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { BranchService } from '../../services/branch.service';
import { createBranchForm, toCreateBranchRequest } from '../../validations/create-branch.form';
import { UppercaseDirective } from '../../../../shared/directives/uppercase';

@Component({
  selector: 'app-create-branch',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, UppercaseDirective],
  templateUrl: './create-branch.html',
  styleUrl: './create-branch.css',
})
export class CreateBranch {
  private _branchService = inject(BranchService);
  private _router = inject(Router);

  form = createBranchForm();
  isSubmitting = false;

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    const request = toCreateBranchRequest(this.form);

    this._branchService.create(request).subscribe({
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

  hasError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return !!(control && control.hasError(errorName) && control.touched);
  }
}
