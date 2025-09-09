import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: false
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  isSubmitting = false;
  errorMessage = '';
  successMessage = '';

  // Available roles
  roles = [
    { value: 'User', label: 'User' },
    { value: 'Admin', label: 'Admin' }
  ];

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      username: ['', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z0-9_]+$/) // Only alphanumeric and underscore
      ]],
      email: ['', [
        Validators.required,
        Validators.email
      ]],
      password: ['', [
        Validators.required,
        Validators.minLength(6),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$/) // At least 1 lowercase, 1 uppercase, 1 number
      ]],
      confirmPassword: ['', Validators.required],

      role: ['User', [Validators.required]]
    }, { 
      validators: this.passwordMatchValidator 
    });
  }

  // Custom validator to check password confirmation
  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password');
    const confirmPassword = form.get('confirmPassword');
    
    if (password && confirmPassword && password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    }
    
    if (confirmPassword?.errors?.['passwordMismatch']) {
      delete confirmPassword.errors['passwordMismatch'];
      if (Object.keys(confirmPassword.errors).length === 0) {
        confirmPassword.setErrors(null);
      }
    }
    
    return null;
  }

  // Getter methods for easy access to form controls in template
  get username() { return this.registerForm.get('username'); }
  get email() { return this.registerForm.get('email'); }
  get password() { return this.registerForm.get('password'); }
  get confirmPassword() { return this.registerForm.get('confirmPassword'); }
  get role() { return this.registerForm.get('role'); }

 onSubmit() {
  if (this.registerForm.valid) {
    this.isSubmitting = true;
    this.errorMessage = '';
    this.successMessage = '';

    // ✅ Create registerData object exactly as expected
    const registerData = {
      username: this.registerForm.value.username,
      email: this.registerForm.value.email,
      password: this.registerForm.value.password,
      role: this.registerForm.value.role
    };

    console.log('🔍 Submitting registration:', registerData);

    // ✅ Call the register method
    this.authService.register(registerData).subscribe({
      next: (response) => {
        console.log('✅ Registration successful:', response);
        this.successMessage = 'Registration successful! Redirecting to login...';
        this.isSubmitting = false;
        
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2000);
      },
      error: (error) => {
        console.error('❌ Registration failed:', error);
        this.isSubmitting = false;
        
        if (error.status === 400) {
          this.errorMessage = 'Registration failed. Please check your information.';
        } else if (error.status === 409) {
          this.errorMessage = 'Username or email already exists.';
        } else {
          this.errorMessage = 'Registration failed. Please try again.';
        }
      }
    });
  }
}


  goToLogin() {
    this.router.navigate(['/login']);
  }
}
