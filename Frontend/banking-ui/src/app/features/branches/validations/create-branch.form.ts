import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CreateBranchRequest } from '../../../core/models/branches';

export type CreateBranchFormContent = {
  name: FormControl<string>;
  code: FormControl<string>;
  street: FormControl<string>;
  city: FormControl<string>;
  country: FormControl<string>;
  zipCode: FormControl<string>;
};

export type CreateBranchFormGroup = FormGroup<CreateBranchFormContent>;

export function createBranchForm(): CreateBranchFormGroup {
  return new FormGroup<CreateBranchFormContent>({
    name: new FormControl<string>('', { nonNullable: true, validators: [Validators.required, Validators.minLength(3)] }),
    code: new FormControl<string>('', { nonNullable: true, validators: [Validators.required, Validators.maxLength(10)] }),
    street: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    city: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    country: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    zipCode: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] })
  });
}

export function toCreateBranchRequest(form: CreateBranchFormGroup): any {
  const raw = form.getRawValue();

  return {
    branchName: raw.name,
    branchCode: raw.code,
    street: raw.street,    
    city: raw.city,
    country: raw.country,
    zipCode: raw.zipCode
  };
}