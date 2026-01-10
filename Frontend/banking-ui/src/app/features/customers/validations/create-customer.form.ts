import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CreateCustomerRequest } from '../../../core/models/customers';
import { CustomValidators } from '../../../shared/validators/custom-validators';

export type CreateCustomerFormContent = {
  firstName: FormControl<string>;
  lastName: FormControl<string>;
  identityNumber: FormControl<string>;
  email: FormControl<string>;
  phone: FormControl<string>;
  street: FormControl<string>;
  city: FormControl<string>;
  country: FormControl<string>;
  zipCode: FormControl<string>;
};

export type CreateCustomerFormGroup = FormGroup<CreateCustomerFormContent>;

export function createCustomerForm(): CreateCustomerFormGroup {
  return new FormGroup<CreateCustomerFormContent>({
    firstName: new FormControl('', { nonNullable: true, validators: [Validators.required, Validators.minLength(2)] }),
    lastName: new FormControl('', { nonNullable: true, validators: [Validators.required, Validators.minLength(2)] }),
    identityNumber: new FormControl('', { nonNullable: true, validators: [Validators.required, CustomValidators.tcKimlikNo] }),
    email: new FormControl('', { nonNullable: true, validators: [Validators.required, Validators.email] }),
    phone: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    street: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    city: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    country: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    zipCode: new FormControl('', { nonNullable: true, validators: [Validators.required] })
  });
}

export function toCreateCustomerRequest(form: CreateCustomerFormGroup): CreateCustomerRequest {
  return form.getRawValue();
}