import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from '../../../shared/validators/custom-validators'; 

// 1. Formun İçeriğini Tanımlıyoruz
export type CreateAccountFormContent = {
  customerId: FormControl<number>;
  branchId: FormControl<number>;
  initialBalance: FormControl<number>;
  currencyCode: FormControl<string>;
};

// 2. FormGroup Tipini Belirliyoruz
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

    initialBalance: new FormControl<number>(0, {
      nonNullable: true,
      validators: [
        Validators.required,
        CustomValidators.greaterThanZero
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
  const randomAccountNumber = Math.floor(1000000000 + Math.random() * 9000000000).toString();

  return {
    customerId: raw.customerId,
    branchId: raw.branchId,
    initialBalance: raw.initialBalance,
    currencyCode: raw.currencyCode,
    accountNumber: randomAccountNumber 
  };
}