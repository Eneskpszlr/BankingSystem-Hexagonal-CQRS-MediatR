import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from '../navbar/navbar';
import { ToastContainer } from '../../shared/components/toast-container/toast-container';

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, Navbar, ToastContainer],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.css',
})
export class MainLayout {

}
