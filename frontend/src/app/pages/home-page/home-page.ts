import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AnnonceService } from '../../services/api/annonce';
import { Annonce, Region, TypeBien } from '../../services/models/annonce.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home-page',
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css'
})
export class HomePage implements OnInit {
  annonces: Annonce[] = [];
  regions: Region[] = [];
  typesBien: TypeBien[] = [];

  filtrePrixMax: number | null = null;
  filtreRegionId: number | null = null;
  filtreTypeBienId: number | null = null;

  constructor(private annonceService: AnnonceService) { }

  ngOnInit(): void {
    this.loadAnnonces();
    this.annonceService.getRegions().subscribe(data => this.regions = data);
    this.annonceService.getTypesBien().subscribe(data => this.typesBien = data);
  }

  loadAnnonces(): void {
    this.annonceService.getAll().subscribe(data => this.annonces = data);
  }

  get annoncesFiltered(): Annonce[] {
    return this.annonces.filter(a => {
      if (this.filtrePrixMax && a.prix > this.filtrePrixMax) return false;
      if (this.filtreRegionId && a.region.id !== this.filtreRegionId) return false;
      if (this.filtreTypeBienId && a.typeBien.id !== this.filtreTypeBienId) return false;
      return true;
    });
  }
}