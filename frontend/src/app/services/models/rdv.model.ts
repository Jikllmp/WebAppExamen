import { Utilisateur } from './utilisateur.model';
import { Annonce } from './annonce.model';

export interface RendezVous {
  id: number;
  dateRdv: string;
  statut: string;
  particulier: Utilisateur;
  annonce: Annonce;
}