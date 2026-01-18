import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusTr',
})
export class StatusTrPipe implements PipeTransform {

  transform(value: string | undefined): string {
    if (!value) return '';

    switch (value) {
      case 'Active': return 'Aktif';
      case 'Passive': return 'Pasif';
      case 'Deposit': return 'Para Yatırma';
      case 'Withdraw': return 'Para Çekme';
      case 'Transfer': return 'Havale';
      case 'IncomingTransfer': return 'Gelen Transfer';
      case 'OutgoingTransfer': return 'Giden Transfer';
      default: return value;
    }
  }
}
