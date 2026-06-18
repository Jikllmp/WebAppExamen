using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class UtilisateurGateway : IUtilisateurGateway
{
    private readonly IUtilisateurRepository _repo;

    public UtilisateurGateway(IUtilisateurRepository repo)
    {
        _repo = repo;
    }

    public Utilisateur? GetUserByEmail(string email)
    {
        var user = _repo.GetByEmail(email);
        if (user == null) return null;

        return new Utilisateur
        {
            Id = user.Id,
            Nom = user.Nom,
            Prenom = user.Prenom,
            Email = user.Email,
            Telephone = user.Tel,
            Role = user.Role,
            NumeroTVA = user.NumTVA
        };
    }

    public string? GetHash(string email)
        => _repo.GetHash(email);

    public void AddUser(string nom, string prenom, string email,
                        string passwordHash, string telephone,
                        string role, string? numeroTVA)
    {
        var user = new Infrastructure.Models.Utilisateur
        {
            Nom = nom,
            Prenom = prenom,
            Email = email,
            MotDePasse = passwordHash,
            Tel = telephone,
            Role = role,
            NumTVA = numeroTVA
        };
        _repo.AddUser(user);
    }
}