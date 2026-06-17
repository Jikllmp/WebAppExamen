using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class RdvUseCases : IRdvUseCases
{
    private readonly IRdvGateway _gateway;

    public RdvUseCases(IRdvGateway gateway)
    {
        _gateway = gateway;
    }

    public void CreateRdv(int particulierId, int annonceId, DateTime dateRdv)
    {
        if (dateRdv <= DateTime.UtcNow)
            throw new ArgumentException("La date du rendez-vous doit être dans le futur.");

        _gateway.AddRdv(dateRdv, particulierId, annonceId);
    }

    public void DeleteRdv(int id_rdv)
        => _gateway.DeleteRdv(id_rdv);

    public IEnumerable<RendezVous> GetAllRdv(int id_user)
        => _gateway.GetRdvParticulier(id_user);

    public IEnumerable<RendezVous> GetAllRdv2(int id_entreprise)
        => _gateway.GetRdvAgence(id_entreprise);
}