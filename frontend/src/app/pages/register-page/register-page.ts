import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthStateService } from '../../services/auth-state';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-register-page',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register-page.html',
  styleUrl: './register-page.css'
})
export class RegisterPageComponent {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authState: AuthStateService
  ) {
    this.registerForm = this.fb.group({
      nom: ['', Validators.required],
      prenom: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      telephone: ['', Validators.required],
      role: ['Particulier', Validators.required],
      numeroTVA: ['']
    });
  }

  get nom() { return this.registerForm.get('nom'); }
  get prenom() { return this.registerForm.get('prenom'); }
  get email() { return this.registerForm.get('email'); }
  get password() { return this.registerForm.get('password'); }
  get telephone() { return this.registerForm.get('telephone'); }
  get role() { return this.registerForm.get('role'); }

  onSubmit() {
    if (this.registerForm.valid) {
      this.authState.register(this.registerForm.value);
    }
  }
}