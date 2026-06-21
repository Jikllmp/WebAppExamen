import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthStateService } from '../../services/auth-state';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login-page',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css'
})
export class LoginPageComponent {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authState: AuthStateService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  get email() { return this.loginForm.get('email'); }
  get password() { return this.loginForm.get('password'); }

  onSubmit() {
    if (this.loginForm.valid) {
      this.authState.login(this.loginForm.value);
    }
  }
}