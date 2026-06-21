import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FavoriService } from '../../services/api/favori';
import { Favori } from '../../services/models/favori.model';

@Component({
  selector: 'app-favoris-page',
  imports: [CommonModule, RouterLink],
  templateUrl: './favoris-page.html',
  styleUrl: './favoris-page.css'
})
export class FavorisPage implements OnInit {
  favoris: Favori[] = [];

  constructor(
    private favoriService: FavoriService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.favoriService.getMesFavoris().subscribe(data => {
      this.favoris = data;
      this.cdr.detectChanges();
    });
  }

  retirer(annonceId: number): void {
    this.favoriService.remove(annonceId).subscribe(() => {
      this.favoris = this.favoris.filter(f => f.annonce.id !== annonceId);
      this.cdr.detectChanges();
    });
  }
}