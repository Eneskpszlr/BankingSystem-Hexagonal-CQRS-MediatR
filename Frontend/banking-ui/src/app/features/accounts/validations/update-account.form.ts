import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UpdateAccountRequest } from '../../../core/models/accounts';

// 1. Form İçeriği
export type UpdateAccountFormContent = {
  id: FormControl<number>;
  name: FormControl<string | null>;
  status: FormControl<string | null>;
};

export type UpdateAccountFormGroup = FormGroup<UpdateAccountFormContent>;

// 2. Form Factory
export function updateAccountForm(): UpdateAccountFormGroup {
  return new FormGroup<UpdateAccountFormContent>({
    
    id: new FormControl<number>(0, {
      nonNullable: true,
      validators: [Validators.required, Validators.min(1)]
    }),

    name: new FormControl<string | null>(null, {
      validators: [
        Validators.minLength(2), 
        Validators.maxLength(100)
      ]
    }),

    status: new FormControl<string | null>(null, {
      validators: [] // Backend enum kontrolü yapacağı için frontend'de required değilse boş geçilebilir
    })
  });
}

// 3. Mapper
export function toUpdateAccountRequest(form: UpdateAccountFormGroup): UpdateAccountRequest {
  const raw = form.getRawValue();

  return {
    id: raw.id,
    name: raw.name ?? undefined,     // Eğer null ise gönderme
    status: raw.status ?? undefined
  };
}