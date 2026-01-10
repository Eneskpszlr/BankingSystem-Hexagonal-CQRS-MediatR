import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TransactionRequest } from '../../../core/models/transactions';
import { CustomValidators } from '../../../shared/validators/custom-validators';

export type TransactionFormContent = {
  accountId: FormControl<number>;
  amount: FormControl<number>;
  description: FormControl<string>;
  currencyCode: FormControl<string>;
};

export type TransactionFormGroup = FormGroup<TransactionFormContent>;

export function createTransactionForm(): TransactionFormGroup {
  return new FormGroup<TransactionFormContent>({
    accountId: new FormControl(0, { nonNullable: true, validators: [Validators.required, Validators.min(1)] }),
    amount: new FormControl(0, { nonNullable: true, validators: [Validators.required, CustomValidators.greaterThanZero] }),
    description: new FormControl('', { nonNullable: true, validators: [Validators.required, Validators.minLength(3)] }),
    currencyCode: new FormControl('TRY', { nonNullable: true, validators: [Validators.required] })
  });
}

export function toTransactionRequest(form: TransactionFormGroup): TransactionRequest {
  return form.getRawValue();
}