import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-confirm-dialog',
  imports: [],
  templateUrl: './confirm-dialog.html',
  styleUrl: './confirm-dialog.css',
})
export class ConfirmDialog {
  @Input() isOpen: boolean = false;
  @Input() title: string = 'Emin misiniz?';
  @Input() message: string = 'Bu işlem geri alınamaz.';
  
  // Parent componente (Sayfaya) haber vermek için Event Fırlatıcılar
  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  onConfirm() {
    this.confirm.emit();
  }

  onCancel() {
    this.cancel.emit();
  }
}
