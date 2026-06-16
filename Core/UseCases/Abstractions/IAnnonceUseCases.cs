using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IAnnonceUseCases
{
    IEnumerable<Annonce> GetAllAnnonce();

    Annonce? GetAnnonceById(int id_annonce);

    void CreateAnnonce(Annonce annonce, int agenceId, IEnumerable<int> commoditeIds);
    void UpdateAnnonce(Annonce annonce, int agenceId, IEnumerable<int> commoditeIds);
    void DeleteAnnonce(int id_annonce);

    IEnumerable<Region> GetRegion();
    IEnumerable<TypeBien> GetTypeBien();
    IEnumerable<Commodite> GetCommodites();
}