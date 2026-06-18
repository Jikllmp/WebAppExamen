using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class RdvGateway : IRdvGateway
{
    private readonly IRdvRepository _repo;

    public RdvGateway(IRdvRepository repo)
    {
        _repo = repo;
    }

    public void AddRdv(DateTime date, int user, int annonce)
        => _repo.Add(user, annonce, date);

    public void DeleteRdv(int id)
        => _repo.Delete(id);

    public IEnumerable<RendezVous> GetRdvParticulier(int id)
        => _repo.GetByParticulier(id).Select(r => new RendezVous
        {
            Id = r.Id,
            DateRdv = r.DateRdv,
            Statut = r.Statut,
            Particulier = new Utilisateur { Id = r.ParticuilierId },
            Annonce = new Annonce { Id = r.AnnonceId }
        });

    public IEnumerable<RendezVous> GetRdvAgence(int id)
        => _repo.GetByAgence(id).Select(r => new RendezVous
        {
            Id = r.Id,
            DateRdv = r.DateRdv,
            Statut = r.Statut,
            Particulier = new Utilisateur { Id = r.ParticuilierId },
            Annonce = new Annonce { Id = r.AnnonceId }
        });
}