import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UpdateCustomerRequest } from '../../../core/models/customers';

export type UpdateCustomerFormContent = {
  id: FormControl<number>;
  email: FormControl<string>;
  phone: FormControl<string>;
  street: FormControl<string>;
  city: FormControl<string>;
  country: FormControl<string>;
  zipCode: FormControl<string>;
};

export type UpdateCustomerFormGroup = FormGroup<UpdateCustomerFormContent>;

export function updateCustomerForm(): UpdateCustomerFormGroup {
  return new FormGroup<UpdateCustomerFormContent>({
    id: new FormControl(0, { nonNullable: true, validators: [Validators.required] }),
    email: new FormControl('', { nonNullable: true, validators: [Validators.required, Validators.email] }),
    phone: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    street: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    city: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    country: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    zipCode: new FormControl('', { nonNullable: true, validators: [Validators.required] })
  });
}

export function toUpdateCustomerRequest(form: UpdateCustomerFormGroup): UpdateCustomerRequest {
  return form.getRawValue();
}