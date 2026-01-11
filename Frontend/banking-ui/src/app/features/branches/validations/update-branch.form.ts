import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UpdateBranchRequest } from '../../../core/models/branches';

export type UpdateBranchFormContent = {
  id: FormControl<number>;
  name: FormControl<string>;
  street: FormControl<string>;
  city: FormControl<string>;
  country: FormControl<string>;
  zipCode: FormControl<string>;
};

export type UpdateBranchFormGroup = FormGroup<UpdateBranchFormContent>;

export function updateBranchForm(): UpdateBranchFormGroup {
  return new FormGroup<UpdateBranchFormContent>({
    id: new FormControl<number>(0, { nonNullable: true, validators: [Validators.required] }),
    name: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    street: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    city: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    country: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    zipCode: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] })
  });
}

export function toUpdateBranchRequest(form: UpdateBranchFormGroup): UpdateBranchRequest {
  const raw = form.getRawValue();
  return {
    id: raw.id,
    branchName: raw.name,
    street: raw.street,
    city: raw.city,
    country: raw.country,
    zipCode: raw.zipCode
  }
}