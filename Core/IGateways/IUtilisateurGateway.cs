using System;
using Core.Models;


namespace Core.IGateways;

public interface IUtilisateurGateway
{
    Utilisateur? GetUserByEmail(string email);

    string? GetHash(string email);
    void AddUser(string nom,string prenom,string email,string telephone,string role,string tva,string passwordhash);
}