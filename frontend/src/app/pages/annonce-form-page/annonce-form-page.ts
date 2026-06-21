import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AnnonceService } from '../../services/api/annonce';
import { Region, TypeBien, Commodite } from '../../services/models/annonce.model';

@Component({
  selector: 'app-annonce-form-page',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './annonce-form-page.html',
  styleUrl: './annonce-form-page.css'
})
export class AnnonceFormPage implements OnInit {
  form: FormGroup;
  regions: Region[] = [];
  typesBien: TypeBien[] = [];
  commodites: Commodite[] = [];
  annonceId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private annonceService: AnnonceService,
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {
    this.form = this.fb.group({
      titre: ['', Validators.required],
      description: ['', Validators.required],
      prix: [0, [Validators.required, Validators.min(1)]],
      nombrePieces: [1, Validators.required],
      superficie: [0, [Validators.required, Validators.min(1)]],
      jardin: [false],
      garage: [false],
      regionId: [null, Validators.required],
      typeBienId: [null, Validators.required],
      commoditeIds: [[]]
    });
  }

  ngOnInit(): void {
    this.annonceService.getRegions().subscribe(data => { this.regions = data; this.cdr.detectChanges(); });
    this.annonceService.getTypesBien().subscribe(data => { this.typesBien = data; this.cdr.detectChanges(); });
    this.annonceService.getCommodites().subscribe(data => { this.commodites = data; this.cdr.detectChanges(); });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.annonceId = Number(id);
      this.annonceService.getById(this.annonceId).subscribe(annonce => {
        this.form.patchValue({
          titre: annonce.titre,
          description: annonce.description,
          prix: annonce.prix,
          nombrePieces: annonce.nombrePieces,
          superficie: annonce.superficie,
          jardin: annonce.jardin,
          garage: annonce.garage,
          regionId: annonce.region.id,
          typeBienId: annonce.typeBien.id,
          commoditeIds: annonce.commodites.map(c => c.id)
        });
        this.cdr.detectChanges();
      });
    }
  }

  toggleCommodite(id: number): void {
    const current: number[] = this.form.value.commoditeIds;
    if (current.includes(id)) {
      this.form.patchValue({ commoditeIds: current.filter(c => c !== id) });
    } else {
      this.form.patchValue({ commoditeIds: [...current, id] });
    }
  }

  isCommoditeChecked(id: number): boolean {
    return this.form.value.commoditeIds.includes(id);
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    const payload = {
      titre: this.form.value.titre,
      description: this.form.value.description,
      prix: this.form.value.prix,
      nombrePieces: this.form.value.nombrePieces,
      superficie: this.form.value.superficie,
      jardin: this.form.value.jardin,
      garage: this.form.value.garage,
      typeBien: { id: this.form.value.typeBienId },
      region: { id: this.form.value.regionId },
      commodites: this.form.value.commoditeIds.map((id: number) => ({ id }))
    };

    if (this.annonceId) {
      this.annonceService.update(this.annonceId, payload).subscribe(() => {
        this.router.navigate(['/annonces', this.annonceId]);
      });
    } else {
      this.annonceService.create(payload).subscribe(() => {
        this.router.navigate(['/']);
      });
    }
  }
}