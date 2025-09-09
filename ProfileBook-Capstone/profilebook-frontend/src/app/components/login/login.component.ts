import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, LoginRequest } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: false
})
export class LoginComponent {
  loginData: LoginRequest = {
    username: '',  // Make sure this matches your backend
    password: ''
  };

  isLoading = false;
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit(): void {
    console.log('Login form submitted with:', this.loginData); // Debug log
    
    if (this.isValidForm()) {
      this.isLoading = true;
      this.errorMessage = '';

      this.authService.login(this.loginData).subscribe({
        next: (response) => {
          
        console.log('✅ Full login response from backend:', response); // Add this line
        console.log('✅ Response type:', typeof response); // Add this line
        console.log('✅ Response.user:', response.user); // Add this line
        console.log('✅ Response.token:', response.token); // Add this line
          this.isLoading = false;
          
          // Navigate to dashboard after successful login
          this.router.navigate(['/dashboard']).then(navigationSuccess => {
            console.log('Navigation to dashboard:', navigationSuccess);
          });
        },
        error: (error) => {
          console.error('❌ Login error:', error); // Debug log
          this.errorMessage = error.error?.message || 'Login failed.'; //Check credentials and backend connection.';
          this.isLoading = false;
        }
      });
    } else {
      console.log('Form validation failed');
    }
  }

  isValidForm(): boolean {
    const isValid = this.loginData.username.trim() !== '' && this.loginData.password.trim() !== '';
    console.log('Form is valid:', isValid, this.loginData);
    return isValid;
  }

  goToRegister(): void {
    this.router.navigate(['/register']);
  }
}
