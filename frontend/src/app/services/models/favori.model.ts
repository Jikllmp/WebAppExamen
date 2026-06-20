import { Utilisateur } from './utilisateur.model';
import { Annonce } from './annonce.model';

export interface Favori {
  utilisateur: Utilisateur;
  annonce: Annonce;
  dateAjout: string;
}