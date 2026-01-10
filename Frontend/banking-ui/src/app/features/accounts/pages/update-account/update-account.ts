import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { updateAccountForm, toUpdateAccountRequest } from '../../validations/update-account.form';
import { Loader } from '../../../../shared/components/loader/loader';

@Component({
  selector: 'app-update-account',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './update-account.html',
  styleUrl: './update-account.css',
})
export class UpdateAccount implements OnInit{
  private _accountService = inject(AccountService);
  private _route = inject(ActivatedRoute);
  private _router = inject(Router);

  // 1. Update Formunu Factory'den üretiyoruz
  form = updateAccountForm();

  // State
  accountId!: number;
  isLoading = true;      // Veriyi çekerken
  isSubmitting = false;  // Kaydederken

  // Backend'den gelen Account verisini ekranda "Read-Only" göstermek için saklayalım
  // (Çünkü formda Balance, Currency yok, ama kullanıcı neyi düzenlediğini bilmeli)
  currentAccountInfo: any = null; 

  ngOnInit(): void {
    // URL'den ID'yi al: /accounts/update/5 -> 5
    const id = this._route.snapshot.paramMap.get('id');
    if (id) {
      this.accountId = Number(id);
      this.loadAccountData(this.accountId);
    }
  }

  // 2. Mevcut Veriyi Getir ve Forma Doldur
  loadAccountData(id: number) {
    this._accountService.getById(id).subscribe({
      next: (response) => {
        const data = response.data;
        this.currentAccountInfo = data;

        // Formu Doldur (Patch Value)
        this.form.patchValue({
          id: data.id,
          // Backend modelinde 'name' (hesap takma adı) varsa buraya gelir, yoksa boş kalır
          // name: data.name, 
          status: data.status
        });

        this.isLoading = false;
      },
      error: (err) => {
        console.error('Hesap bulunamadı', err);
        this.isLoading = false;
        // Hata varsa listeye geri atabiliriz
        this._router.navigate(['/accounts']);
      }
    });
  }

  // 3. Güncelle Butonuna Basınca
  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    // Factory Mapper ile DTO oluştur
    const request = toUpdateAccountRequest(this.form);

    this._accountService.update(request).subscribe({
      next: () => {
        console.log('Güncelleme başarılı');
        this.isSubmitting = false;
        this._router.navigate(['/accounts']);
      },
      error: (err) => {
        console.error('Güncelleme hatası', err);
        this.isSubmitting = false;
      }
    });
  }
}
