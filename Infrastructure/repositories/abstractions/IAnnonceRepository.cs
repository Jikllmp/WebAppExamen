using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IAnnonceRepository
{
    IEnumerable<Annonce> GetAll();
    Annonce? GetById(int id);
    void Add(Annonce annonce, int agenceId, IEnumerable<int> commoditeIds);
    void Update(Annonce annonce, IEnumerable<int> commoditeIds);
    void Delete(int id);
    IEnumerable<Region> GetAllRegions();
    IEnumerable<TypeB> GetAllTypesBien();
    IEnumerable<Commodite> GetAllCommodites();
}