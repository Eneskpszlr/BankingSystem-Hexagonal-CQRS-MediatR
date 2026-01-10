import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export class CustomValidators {

  /**
   * Sadece boşluk karakteri girilmesini engeller.
   * Örn: "   " -> Hata verir.
   */
  static noWhitespace(control: AbstractControl): ValidationErrors | null {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  }

  /**
   * Sayının sıfırdan büyük olup olmadığını kontrol eder.
   */
  static greaterThanZero(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    if (value !== null && value !== undefined && value <= 0) {
      return { notGreaterThanZero: true };
    }
    return null;
  }

  /**
   * TC Kimlik No Algoritma Kontrolü
   * 11 hane olmalı ve sayısal olmalı.
   */
  static tcKimlikNo(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    const valid = /^[1-9]{1}[0-9]{9}[0,2,4,6,8]{1}$/.test(value);
    
    if (!value) return null; // Boşsa required validatörü baksın
    return valid ? null : { invalidTc: true };
  }
  
  static match(controlName: string, matchingControlName: string): ValidatorFn {
    return (group: AbstractControl): ValidationErrors | null => {
      const control = group.get(controlName);
      const matchingControl = group.get(matchingControlName);

      if (!control || !matchingControl) {
        return null;
      }

      if (matchingControl.errors && !matchingControl.errors['mustMatch']) {
        return null;
      }

      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
        return { mustMatch: true };
      } else {
        matchingControl.setErrors(null);
        return null;
      }
    };
  }
}