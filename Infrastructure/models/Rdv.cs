namespace Infrastructure.Models;

public class RendezVous
{
    public int Id { get; set; }
    public DateTime DateRdv { get; set; }
    public string Statut { get; set; } = "EnAttente";
    public int ParticuilierId { get; set; }
    public int AnnonceId { get; set; }
}