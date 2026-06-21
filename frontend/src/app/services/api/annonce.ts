import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Annonce, Region, TypeBien, Commodite } from '../models/annonce.model';

@Injectable({
  providedIn: 'root'
})
export class AnnonceService {
  private apiUrl = 'http://localhost:5202/api/annonces';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Annonce[]> {
    return this.http.get<Annonce[]>(this.apiUrl);
  }

  getById(id: number): Observable<Annonce> {
    return this.http.get<Annonce>(`${this.apiUrl}/${id}`);
  }

  create(annonce: Partial<Annonce>): Observable<any> {
    return this.http.post(this.apiUrl, annonce);
  }

  update(id: number, annonce: Partial<Annonce>): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, annonce);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getRegions(): Observable<Region[]> {
    return this.http.get<Region[]>(`${this.apiUrl}/regions`);
  }

  getTypesBien(): Observable<TypeBien[]> {
    return this.http.get<TypeBien[]>(`${this.apiUrl}/types-bien`);
  }

  getCommodites(): Observable<Commodite[]> {
    return this.http.get<Commodite[]>(`${this.apiUrl}/commodites`);
  }
}