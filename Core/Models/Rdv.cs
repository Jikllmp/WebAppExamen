namespace Core.Models;

public class RendezVous
{
    public int Id { get; set; }
    public DateTime DateRdv { get; set; }
    public string Statut { get; set; } = "EnAttente";
    public Utilisateur Particulier { get; set; } = new();
    public Annonce Annonce { get; set; } = new();
}