using Core.Models;
using Core.UseCases.Abstractions;
using Core.IGateways;

namespace Core.UseCases;

public class UtilisateurUseCases : IUtilisateurUseCases
{
    private readonly IUtilisateurGateway gateway;

    public UtilisateurUseCases(IUtilisateurGateway gateway2)
    {
        gateway = gateway2;
    }

    public Utilisateur Connection(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Email et mot de passe requis.");

        var hash = gateway.GetHash(email);
        if (hash == null)
            throw new ArgumentException("Email ou mot de passe incorrect.");

        if (!BCrypt.Net.BCrypt.Verify(password, hash))
            throw new ArgumentException("Email ou mot de passe incorrect.");

        return gateway.GetUserByEmail(email)!;
    }

    public void Inscrire(string nom, string prenom, string email,string password, string telephone,string role, string? numeroTVA)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Email et mot de passe requis.");

        if (role == "Agence" && string.IsNullOrWhiteSpace(numeroTVA))
            throw new ArgumentException("Numéro de TVA obligatoire pour une agence.");

        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        gateway.AddUser(nom, prenom, email, hash, telephone, role, numeroTVA);
    }
}