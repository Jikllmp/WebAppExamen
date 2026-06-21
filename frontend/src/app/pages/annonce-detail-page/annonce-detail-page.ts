import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { AnnonceService } from '../../services/api/annonce';
import { Annonce } from '../../services/models/annonce.model';
import { AuthStateService } from '../../services/auth-state';
import { Rdv } from '../../services/api/rdv';
import { FavoriService } from '../../services/api/favori';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-annonce-detail-page',
  imports: [CommonModule, FormsModule],
  templateUrl: './annonce-detail-page.html',
  styleUrl: './annonce-detail-page.css'
})
export class AnnonceDetailPage implements OnInit {
  annonce: Annonce | null = null;
  dateRdv: string = '';
  message: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private annonceService: AnnonceService,
    public authState: AuthStateService,
    private rdvService: Rdv,
    private favoriService: FavoriService
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.annonceService.getById(id).subscribe(data => this.annonce = data);
  }

  prendreRdv(): void {
    if (!this.authState.token) {
      this.router.navigate(['/login']);
      return;
    }

    if (!this.dateRdv || !this.annonce) {
      this.message = 'Choisis une date.';
      return;
    }

    this.rdvService.create(this.annonce.id, this.dateRdv).subscribe({
      next: () => this.message = 'Rendez-vous demandé avec succès !',
      error: () => this.message = 'Erreur lors de la prise de rendez-vous.'
    });
  }

  ajouterFavori(): void {
    if (!this.authState.token) {
      this.router.navigate(['/login']);
      return;
    }

    if (!this.annonce) return;

    this.favoriService.add(this.annonce.id).subscribe({
      next: () => this.message = 'Ajouté aux favoris !',
      error: () => this.message = 'Erreur lors de l\'ajout aux favoris.'
    });
  }
}