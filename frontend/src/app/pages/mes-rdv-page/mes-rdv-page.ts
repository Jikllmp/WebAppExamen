import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Rdv } from '../../services/api/rdv';
import { RendezVous } from '../../services/models/rdv.model';
import { AuthStateService } from '../../services/auth-state';

@Component({
  selector: 'app-mes-rdv-page',
  imports: [CommonModule, RouterLink],
  templateUrl: './mes-rdv-page.html',
  styleUrl: './mes-rdv-page.css'
})
export class MesRdvPage implements OnInit {
  rdvs: RendezVous[] = [];
  isAgence: boolean = false;

  constructor(
    private rdvService: Rdv,
    private authState: AuthStateService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.isAgence = this.authState.getRole() === 'Agence';

    if (this.isAgence) {
      this.rdvService.getRdvAgence().subscribe(data => {
        this.rdvs = data;
        this.cdr.detectChanges();
      });
    } else {
      this.rdvService.getMesRdv().subscribe(data => {
        this.rdvs = data;
        this.cdr.detectChanges();
      });
    }
  }

  annuler(id: number): void {
    this.rdvService.cancel(id).subscribe(() => {
      this.rdvs = this.rdvs.filter(r => r.id !== id);
      this.cdr.detectChanges();
    });
  }
}