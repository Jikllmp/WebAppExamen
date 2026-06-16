using Core.Models;

namespace Core.UseCases.Abstractions;


public interface IFavoriUseCases
{
    void AddFavori(int id_user, int id_annonce);
    void DeleteFavori(int id_favori);
    IEnumerable<Favori> GetFavori(int id_user);
}