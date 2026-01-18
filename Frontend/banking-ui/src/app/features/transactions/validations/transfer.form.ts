import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TransferRequest } from '../../../core/models/transactions';
import { CustomValidators } from '../../../shared/validators/custom-validators';

// 1. Formun İçeriği
export type TransferFormContent = {
  accountId: FormControl<number>;
  targetAccountId: FormControl<number>;
  amount: FormControl<number>;
  description: FormControl<string>;
  currencyCode: FormControl<string>;
};

export type TransferFormGroup = FormGroup<TransferFormContent>;

// 2. Formu Oluşturan Fonksiyon
export function createTransferForm(): TransferFormGroup {
  return new FormGroup<TransferFormContent>({
    accountId: new FormControl(0, { nonNullable: true, validators: [Validators.required, Validators.min(1)] }),
    targetAccountId: new FormControl(0, { nonNullable: true, validators: [Validators.required, Validators.min(1)] }),
    amount: new FormControl(0, { nonNullable: true, validators: [Validators.required, CustomValidators.greaterThanZero] }),
    description: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    currencyCode: new FormControl('TRY', { nonNullable: true, validators: [Validators.required] })
  });
}

// 3. Form Verisini Backend Modeline Dönüştüren Fonksiyon (Mapper)
export function toTransferRequest(form: TransferFormGroup): TransferRequest {
  const raw = form.getRawValue();

  return {
    fromAccountId: raw.accountId,
    amount: raw.amount,
    currencyCode: raw.currencyCode,
    description: raw.description,
    toAccountNumber: String(raw.targetAccountId) 
  };
}