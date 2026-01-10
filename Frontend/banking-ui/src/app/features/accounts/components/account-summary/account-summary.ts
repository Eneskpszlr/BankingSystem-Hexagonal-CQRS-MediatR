import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StatusTrPipe } from '../../../../shared/pipes/status-tr-pipe';

@Component({
  selector: 'app-account-summary',
  imports: [CommonModule, StatusTrPipe],
  templateUrl: './account-summary.html',
  styleUrl: './account-summary.css',
})
export class AccountSummary {
  @Input() account: any;
}
