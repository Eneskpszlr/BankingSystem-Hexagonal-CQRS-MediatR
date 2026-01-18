import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-confirm-dialog',
  imports: [CommonModule],
  templateUrl: './confirm-dialog.html',
  styleUrl: './confirm-dialog.css',
})
export class ConfirmDialog {
  @Input() isOpen: boolean = false;
  @Input() title: string = 'Emin misiniz?';
  @Input() message: string = 'Bu işlem geri alınamaz.';
  @Input() confirmText: string = 'Onayla';
  @Input() cancelText: string = 'Vazgeç';
  @Input() type: 'danger' | 'primary' | 'success' = 'danger';
  
  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  onConfirm() {
    this.confirm.emit();
  }

  onCancel() {
    this.cancel.emit();
  }
}