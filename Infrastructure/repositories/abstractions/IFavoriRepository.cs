using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IFavoriRepository
{
    void Add(int userId, int annonceId);
    void Delete(int userId, int annonceId);
    IEnumerable<Favori> GetByUser(int userId);
}