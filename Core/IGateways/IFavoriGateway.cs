using System.IO.Compression;
using Core.Models;


namespace Core.IGateways;


public interface IFavoriGateway
{
    void AddFavoris(int id_annonce,int id_user);
    
    void DeleteFavori(int id_annonce,int id_user);

    IEnumerable<Favori> GetFavoris(int user);
}