import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  standalone: false
})
export class DashboardComponent implements OnInit {
  currentUser: any;
  activeTab = 'feed';  // Default to feed tab
  isAdmin = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.currentUser = this.authService.getCurrentUser();
    this.isAdmin = this.authService.isAdmin();
    
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
    }
  }

  setActiveTab(tab: string) {
    this.activeTab = tab;
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
