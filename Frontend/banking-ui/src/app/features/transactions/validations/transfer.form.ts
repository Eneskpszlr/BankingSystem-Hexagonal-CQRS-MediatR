import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TransferRequest } from '../../../core/models/transactions';
import { CustomValidators } from '../../../shared/validators/custom-validators';

export type TransferFormContent = {
  accountId: FormControl<number>;
  targetAccountId: FormControl<number>;
  amount: FormControl<number>;
  description: FormControl<string>;
  currencyCode: FormControl<string>;
};

export type TransferFormGroup = FormGroup<TransferFormContent>;

export function createTransferForm(): TransferFormGroup {
  return new FormGroup<TransferFormContent>({
    accountId: new FormControl(0, { nonNullable: true, validators: [Validators.required, Validators.min(1)] }),
    targetAccountId: new FormControl(0, { nonNullable: true, validators: [Validators.required, Validators.min(1)] }),
    amount: new FormControl(0, { nonNullable: true, validators: [Validators.required, CustomValidators.greaterThanZero] }),
    description: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    currencyCode: new FormControl('TRY', { nonNullable: true, validators: [Validators.required] })
  });
}

export function toTransferRequest(form: TransferFormGroup): TransferRequest {
  return form.getRawValue();
}