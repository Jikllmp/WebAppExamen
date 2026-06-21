import { Routes } from '@angular/router';
import { HomePage } from './pages/home-page/home-page';
import { LoginPageComponent } from './pages/login-page/login-page';
import { RegisterPageComponent } from './pages/register-page/register-page';
import { AnnonceDetailPage } from './pages/annonce-detail-page/annonce-detail-page';
import { FavorisPage } from './pages/favoris-page/favoris-page';
import { MesRdvPage } from './pages/mes-rdv-page/mes-rdv-page';
import { AnnonceFormPage } from './pages/annonce-form-page/annonce-form-page';


export const routes: Routes = [
  { path: '', component: HomePage },
  { path: 'login', component: LoginPageComponent },
  { path: 'register', component: RegisterPageComponent },
  { path: 'favoris', component: FavorisPage },
  { path: 'mes-rdv', component: MesRdvPage },
  { path: 'annonces/nouvelle', component: AnnonceFormPage },
  { path: 'annonces/:id/modifier', component: AnnonceFormPage },
  { path: 'annonces/:id', component: AnnonceDetailPage }
];