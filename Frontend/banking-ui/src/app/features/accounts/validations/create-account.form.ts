import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from '../../../shared/validators/custom-validators'; 

// 1. Formun İçeriği
export type CreateAccountFormContent = {
  customerId: FormControl<number>;
  branchId: FormControl<number>;
  currencyCode: FormControl<string>;
};

// 2. FormGroup Tipi
export type CreateAccountFormGroup = FormGroup<CreateAccountFormContent>;

// 3. Formu Üreten Factory Fonksiyonu
export function createAccountForm(): CreateAccountFormGroup {
  return new FormGroup<CreateAccountFormContent>({
    
    customerId: new FormControl<number>(0, {
      nonNullable: true,
      validators: [
        Validators.required, 
        Validators.min(1)
      ]
    }),

    branchId: new FormControl<number>(0, {
      nonNullable: true,
      validators: [
        Validators.required, 
        Validators.min(1)
      ]
    }),

    currencyCode: new FormControl<string>('TRY', {
      nonNullable: true,
      validators: [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(3)
      ]
    })
  });
}

// 4. Form Verisini Backend DTO'suna Çeviren Mapper
export function toCreateAccountRequest(form: CreateAccountFormGroup): any {
  const raw = form.getRawValue();

  return {
    customerId: raw.customerId,
    branchId: raw.branchId,
    currencyCode: raw.currencyCode,
  };
}