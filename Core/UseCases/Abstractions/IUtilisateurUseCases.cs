using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IUtilisateurUseCases
{
    Utilisateur Connection(string email, string password);

    void Inscrire(string nom, string prenom, string email, 
                  string password, string telephone, 
                  string role, string? numeroTVA);
}