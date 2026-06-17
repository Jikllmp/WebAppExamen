using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class AnnonceUseCases : IAnnonceUseCases
{
    private readonly IAnnonceGateway _gateway;

    public AnnonceUseCases(IAnnonceGateway gateway)
    {
        _gateway = gateway;
    }

    public IEnumerable<Annonce> GetAllAnnonce()
        => _gateway.GetAllAnnonce();

    public Annonce? GetAnnonceById(int id)
        => _gateway.GetAnnonceById(id);

    public void CreateAnnonce(Annonce annonce, int agenceId, IEnumerable<int> commoditeIds)
    {
        if (string.IsNullOrWhiteSpace(annonce.Titre))
            throw new ArgumentException("Le titre est obligatoire.");
        if (annonce.Prix <= 0)
            throw new ArgumentException("Le prix doit être supérieur à zéro.");
        if (annonce.Superficie <= 0)
            throw new ArgumentException("La superficie doit être supérieure à zéro.");

        _gateway.AddAnnonce(annonce, agenceId, commoditeIds);
    }

    public void UpdateAnnonce(Annonce annonce, int agenceId, IEnumerable<int> commoditeIds)
    {
        var existing = _gateway.GetAnnonceById(annonce.Id)
            ?? throw new KeyNotFoundException("Annonce introuvable.");

        if (existing.Agence.Id != agenceId)
            throw new UnauthorizedAccessException("Vous ne pouvez modifier que vos propres annonces.");

        _gateway.UpdateAnnonce(annonce, commoditeIds);
    }

    public void DeleteAnnonce(int id, int agenceId)
    {
        var existing = _gateway.GetAnnonceById(id)
            ?? throw new KeyNotFoundException("Annonce introuvable.");

        if (existing.Agence.Id != agenceId)
            throw new UnauthorizedAccessException("Vous ne pouvez supprimer que vos propres annonces.");

        _gateway.DeleteAnnonce(id);
    }

    public IEnumerable<Region> GetRegion() => _gateway.GetAllRegion();
    public IEnumerable<TypeBien> GetTypeBien() => _gateway.GetAllTypeBien();
    public IEnumerable<Commodite> GetCommodites() => _gateway.GetAllCommodite();
}