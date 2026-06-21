import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
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
    private favoriService: FavoriService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.annonceService.getById(id).subscribe(data => {
      this.annonce = data;
      this.cdr.detectChanges();
    });
  }

  prendreRdv(): void {
    if (!this.authState.token) {
      this.router.navigate(['/login']);
      return;
    }

    if (!this.dateRdv || !this.annonce) {
      this.message = 'Choisis une date.';
      this.cdr.detectChanges();
      return;
    }

    this.rdvService.create(this.annonce.id, this.dateRdv).subscribe({
      next: () => { this.message = 'Rendez-vous demandé avec succès !'; this.cdr.detectChanges(); },
      error: () => { this.message = 'Erreur lors de la prise de rendez-vous.'; this.cdr.detectChanges(); }
    });
  }

  ajouterFavori(): void {
    if (!this.authState.token) {
      this.router.navigate(['/login']);
      return;
    }

    if (!this.annonce) return;

    this.favoriService.add(this.annonce.id).subscribe({
      next: () => { this.message = 'Ajouté aux favoris !'; this.cdr.detectChanges(); },
      error: () => { this.message = 'Erreur lors de l\'ajout aux favoris.'; this.cdr.detectChanges(); }
    });
  }
}