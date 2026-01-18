import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'app-loader',
  imports: [],
  templateUrl: './loader.html',
  styleUrl: './loader.css',
  // Performans: Sadece input değişince çalışsın
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Loader {
  @Input() isLoading: boolean = false;
}
