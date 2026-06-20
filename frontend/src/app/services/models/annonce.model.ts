import { Utilisateur } from './utilisateur.model';

export interface Region {
  id: number;
  nom: string;
}

export interface TypeBien {
  id: number;
  libelle: string;
}

export interface Commodite {
  id: number;
  libelle: string;
}

export interface Annonce {
  id: number;
  titre: string;
  description: string;
  prix: number;
  nombrePieces: number;
  superficie: number;
  jardin: boolean;
  garage: boolean;
  datePublication: string;
  agence: Utilisateur;
  typeBien: TypeBien;
  region: Region;
  commodites: Commodite[];
}