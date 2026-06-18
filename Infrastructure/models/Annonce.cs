namespace Infrastructure.Models;

public class Annonce
{
    public int Id { get; set; }
    public string Tite { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Prix { get; set; }
    public int NbPieces { get; set; }
    public decimal Superficie { get; set; }
    public bool Jardin { get; set; }
    public bool Garage { get; set; }
    public DateTime DatePublication { get; set; }
    public int AgenceId { get; set; }
    public int TypeBienId { get; set; }
    public int RegionId { get; set; }
}