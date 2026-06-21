import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RendezVous } from '../models/rdv.model';

@Injectable({
  providedIn: 'root'
})
export class Rdv {
  private apiUrl = 'http://localhost:5202/api/rdv';

  constructor(private http: HttpClient) { }

  create(annonceId: number, dateRdv: string): Observable<any> {
    return this.http.post(this.apiUrl, { annonceId, dateRdv });
  }

  cancel(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getMesRdv(): Observable<RendezVous[]> {
    return this.http.get<RendezVous[]>(`${this.apiUrl}/mes-rdv`);
  }

  getRdvAgence(): Observable<RendezVous[]> {
    return this.http.get<RendezVous[]>(`${this.apiUrl}/agence`);
  }
}