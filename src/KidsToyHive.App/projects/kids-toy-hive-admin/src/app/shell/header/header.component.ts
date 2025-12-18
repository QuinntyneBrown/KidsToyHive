// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, Output, EventEmitter, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDividerModule } from '@angular/material/divider';
import { AuthService } from '../../core/services/auth.service';
import { ThemeService } from '../../core/services/theme.service';

/**
 * Application header with logo, menu toggle, user menu, and theme toggle
 */
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatBadgeModule,
    MatTooltipModule,
    MatDividerModule
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  private readonly authService = inject(AuthService);
  private readonly themeService = inject(ThemeService);
  private readonly router = inject(Router);

  @Output() menuToggle = new EventEmitter<void>();

  currentUser$ = this.authService.currentUser$;
  isDarkTheme = this.themeService.isDarkTheme;

  /**
   * Toggle sidebar menu
   */
  onMenuToggle(): void {
    this.menuToggle.emit();
  }

  /**
   * Navigate to dashboard
   */
  onLogoClick(): void {
    this.router.navigate(['/dashboard']);
  }

  /**
   * Toggle theme between light and dark
   */
  onThemeToggle(): void {
    this.themeService.toggleTheme();
  }

  /**
   * Navigate to profile
   */
  onProfile(): void {
    this.router.navigate(['/profile']);
  }

  /**
   * Navigate to settings
   */
  onSettings(): void {
    this.router.navigate(['/settings']);
  }

  /**
   * Show help
   */
  onHelp(): void {
    // TODO: Implement help functionality
    // Could navigate to help page or open help dialog
  }

  /**
   * Logout user
   */
  onLogout(): void {
    this.authService.logout();
  }

  /**
   * Get user initials for avatar
   */
  getUserInitials(name: string): string {
    if (!name) return '';
    
    const parts = name.split(' ');
    if (parts.length >= 2) {
      return `${parts[0][0]}${parts[1][0]}`.toUpperCase();
    }
    return name.substring(0, 2).toUpperCase();
  }
}
