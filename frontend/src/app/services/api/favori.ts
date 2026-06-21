import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Favori } from '../models/favori.model';

@Injectable({
  providedIn: 'root'
})
export class FavoriService {
  private apiUrl = 'http://localhost:5202/api/favoris';

  constructor(private http: HttpClient) { }

  add(annonceId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/${annonceId}`, {});
  }

  remove(annonceId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${annonceId}`);
  }

  getMesFavoris(): Observable<Favori[]> {
    return this.http.get<Favori[]>(this.apiUrl);
  }
}