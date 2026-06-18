using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class AnnonceGateway : IAnnonceGateway
{
    private readonly IAnnonceRepository _repo;

    public AnnonceGateway(IAnnonceRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<Core.Models.Annonce> GetAllAnnonce()
    {
        var annonces = _repo.GetAll();
        return annonces.Select(a => new Core.Models.Annonce
        {
            Id = a.Id,
            Titre = a.Tite,
            Description = a.Description,
            Prix = a.Prix,
            NombrePieces = a.NbPieces,
            Superficie = a.Superficie,
            Jardin = a.Jardin,
            Garage = a.Garage,
            DatePublication = a.DatePublication,
            Agence = new Core.Models.Utilisateur { Id = a.AgenceId },
            TypeBien = new Core.Models.TypeBien { Id = a.TypeBienId },
            Region = new Core.Models.Region { Id = a.RegionId }
        });
    }

    public Core.Models.Annonce? GetAnnonceById(int id)
    {
        var a = _repo.GetById(id);
        if (a == null) return null;
        return new Core.Models.Annonce
        {
            Id = a.Id,
            Titre = a.Tite,
            Description = a.Description,
            Prix = a.Prix,
            NombrePieces = a.NbPieces,
            Superficie = a.Superficie,
            Jardin = a.Jardin,
            Garage = a.Garage,
            DatePublication = a.DatePublication,
            Agence = new Core.Models.Utilisateur { Id = a.AgenceId },
            TypeBien = new Core.Models.TypeBien { Id = a.TypeBienId },
            Region = new Core.Models.Region { Id = a.RegionId }
        };
    }

    public void AddAnnonce(Core.Models.Annonce annonce, int agenceId, IEnumerable<int> commoditeIds)
    {
        var infraAnnonce = new Infrastructure.Models.Annonce
        {
            Tite = annonce.Titre,
            Description = annonce.Description,
            Prix = annonce.Prix,
            NbPieces = annonce.NombrePieces,
            Superficie = annonce.Superficie,
            Jardin = annonce.Jardin,
            Garage = annonce.Garage,
            TypeBienId = annonce.TypeBien.Id,
            RegionId = annonce.Region.Id
        };
        _repo.Add(infraAnnonce, agenceId, commoditeIds);
    }

    public void UpdateAnnonce(Core.Models.Annonce annonce, IEnumerable<int> commoditeIds)
    {
        var infraAnnonce = new Infrastructure.Models.Annonce
        {
            Id = annonce.Id,
            Tite = annonce.Titre,
            Description = annonce.Description,
            Prix = annonce.Prix,
            NbPieces = annonce.NombrePieces,
            Superficie = annonce.Superficie,
            Jardin = annonce.Jardin,
            Garage = annonce.Garage,
            TypeBienId = annonce.TypeBien.Id,
            RegionId = annonce.Region.Id
        };
        _repo.Update(infraAnnonce, commoditeIds);
    }

    public void DeleteAnnonce(int id)
        => _repo.Delete(id);

    public IEnumerable<Core.Models.Region> GetAllRegion()
        => _repo.GetAllRegions().Select(r => new Core.Models.Region { Id = r.Id, Nom = r.Nom });

    public IEnumerable<Core.Models.TypeBien> GetAllTypeBien()
        => _repo.GetAllTypesBien().Select(t => new Core.Models.TypeBien { Id = t.Id, Libelle = t.Libele });

    public IEnumerable<Core.Models.Commodite> GetAllCommodite()
        => _repo.GetAllCommodites().Select(c => new Core.Models.Commodite { Id = c.Id, Libelle = c.Libele });
}