using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class FavoriGateway : IFavoriGateway
{
    private readonly IFavoriRepository _repo;

    public FavoriGateway(IFavoriRepository repo)
    {
        _repo = repo;
    }

    public void AddFavoris(int id_annonce, int id_user)
        => _repo.Add(id_user, id_annonce);

    public void DeleteFavori(int id_annonce, int id_user)
        => _repo.Delete(id_user, id_annonce);

    public IEnumerable<Favori> GetFavoris(int user)
        => _repo.GetByUser(user).Select(f => new Favori
        {
            Utilisateur = new Utilisateur { Id = f.UserId },
            Annonce = new Annonce { Id = f.AnnonceId },
            DateAjout = f.DateAjout
        });
}