namespace Core.Models;

public class Favori
{
    public Utilisateur Utilisateur { get; set; } = new();
    public Annonce Annonce { get; set; } = new();
    public DateTime DateAjout { get; set; }
}