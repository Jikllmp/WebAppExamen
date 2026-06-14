using System.IO.Compression;
using Core.Models;


namespace Core.IGateways;


public interface IAnnonceGateway
{
    
    Annonce? GetAnnonceById(int id);

    void AddAnnonce(Annonce annonce, int agenceId, IEnumerable<int> commoditeIds);
    void UpdateAnnonce(Annonce annonce, IEnumerable<int> commoditeIds);
    void DeleteAnnonce(int id);

    IEnumerable<Annonce> GetAllAnnonce();
    IEnumerable<Region> GetAllRegion();
    IEnumerable<TypeBien> GetAllTypeBien();
    IEnumerable<Commodite> GetAllCommodite();

    //ici cv pour recupere tout sans filte a faire apres avec filrte
}