namespace Core.Models;

public class Annonce
{
    public int Id { get; set; }
    public string Titre { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Prix { get; set; }
    public int NombrePieces { get; set; }
    public decimal Superficie { get; set; }
    public bool Jardin { get; set; }
    public bool Garage { get; set; }
    public DateTime DatePublication { get; set; }
    public Utilisateur Agence { get; set; } = new();
    public TypeBien TypeBien { get; set; } = new();
    public Region Region { get; set; } = new();
    public IEnumerable<Commodite> Commodites { get; set; } = [];
}