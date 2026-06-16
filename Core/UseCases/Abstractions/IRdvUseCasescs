using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IRdvUseCases
{
    void CreateRdv(int particulierId, int annonceId, DateTime dateRdv);
    void DeleteRdv(int id_rdv);

    IEnumerable<RendezVous> GetAllRdv(int id_user);

    IEnumerable<RendezVous>  GetAllRdv2(int id_entreprise);
}