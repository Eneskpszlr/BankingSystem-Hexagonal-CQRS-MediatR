import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StatusTrPipe } from '../../../../shared/pipes/status-tr-pipe';
import { Account } from '../../../../core/models/accounts';

@Component({
  selector: 'app-account-summary',
  standalone: true,
  imports: [CommonModule, StatusTrPipe],
  templateUrl: './account-summary.html',
  styleUrl: './account-summary.css',
  // Performans için, sadece veri değişince render et
  changeDetection: ChangeDetectionStrategy.OnPush 
})
export class AccountSummary {
  @Input() account: Account | null = null; 
}