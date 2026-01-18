import { Directive, ElementRef, HostListener } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
  selector: '[appUppercase]', // Kullanırken bu ismi yazacağız
  standalone: true
})
export class UppercaseDirective {

  constructor(private el: ElementRef, private control: NgControl) {}

  @HostListener('input', ['$event']) onInputChange(event: any) {
    const initialValue = this.el.nativeElement.value;
    
    // Değeri büyük harfe çevir
    const newValue = initialValue.toUpperCase();

    // 1. Ekranda görüneni güncelle
    this.el.nativeElement.value = newValue;

    // 2. Form Control'ün (Reactive Forms) içindeki değeri güncelle
    if (this.control && this.control.control) {
      this.control.control.setValue(newValue, { emitEvent: false });
    }
  }
}