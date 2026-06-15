using System.IO.Compression;
using Core.Models;


namespace Core.IGateways;


public interface IRdvGateway
{
    void AddRdv(DateTime date,int user,int annonce);
    void DeleteRdv(int id);
    IEnumerable<RendezVous> GetRdvParticulier(int id);
    IEnumerable<RendezVous> GetRdvAgence(int id);
}