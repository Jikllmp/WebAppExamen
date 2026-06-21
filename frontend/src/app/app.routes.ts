import { Routes } from '@angular/router';
import { HomePage } from './pages/home-page/home-page';
import { LoginPageComponent } from './pages/login-page/login-page';
import { RegisterPageComponent } from './pages/register-page/register-page';
import { AnnonceDetailPage } from './pages/annonce-detail-page/annonce-detail-page';

export const routes: Routes = [
  { path: '', component: HomePage },
  { path: 'login', component: LoginPageComponent },
  { path: 'register', component: RegisterPageComponent },
  { path: 'annonces/:id', component: AnnonceDetailPage }
];