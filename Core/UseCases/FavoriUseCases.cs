using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class FavoriUseCases : IFavoriUseCases
{
    private readonly IFavoriGateway _gateway;

    public FavoriUseCases(IFavoriGateway gateway)
    {
        _gateway = gateway;
    }

    public void AddFavori(int id_user, int id_annonce)
        => _gateway.AddFavoris(id_annonce, id_user);

    public void DeleteFavori(int id_favori)
        => _gateway.DeleteFavori(id_favori, 0);

    public IEnumerable<Favori> GetFavori(int id_user)
        => _gateway.GetFavoris(id_user);
}