using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IUtilisateurRepository
{
    Utilisateur? GetByEmail(string email);
    string? GetHash(string email);
    void AddUser(Utilisateur user);
}