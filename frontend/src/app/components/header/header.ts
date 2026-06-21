import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthStateService } from '../../services/auth-state';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  imports: [RouterLink, CommonModule],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class HeaderComponent {
  constructor(public authState: AuthStateService) { }

  logout() {
    this.authState.logout();
  }

  getRole(): string | null {
    return this.authState.getRole();
  }
}