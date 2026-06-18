using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IRdvRepository
{
    void Add(int particulierId, int annonceId, DateTime dateRdv);
    void Delete(int id);
    IEnumerable<RendezVous> GetByParticulier(int particulierId);
    IEnumerable<RendezVous> GetByAgence(int agenceId);
}