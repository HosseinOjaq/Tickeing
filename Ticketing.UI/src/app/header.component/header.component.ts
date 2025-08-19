import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent { 
  auth = inject(AuthService);
  constructor(private authService: AuthService) {
    
  }

  logout() {
    this.authService.logout();
  }
}