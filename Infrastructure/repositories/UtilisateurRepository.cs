using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class UtilisateurRepository : IUtilisateurRepository
{
    private readonly string _connectionString;

    public UtilisateurRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string manquante");
    }

    private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

    public Utilisateur? GetByEmail(string email)
    {
        const string sql = "SELECT * FROM Utilisateur WHERE Email = @Email";
        using var conn = CreateConnection();
        return conn.QuerySingleOrDefault<Utilisateur>(sql, new { Email = email });
    }

    public string? GetHash(string email)
    {
        const string sql = "SELECT MotDePasse FROM Utilisateur WHERE Email = @Email";
        using var conn = CreateConnection();
        return conn.QuerySingleOrDefault<string>(sql, new { Email = email });
    }

    public void AddUser(Utilisateur user)
    {
        const string sql = @"INSERT INTO Utilisateur 
            (Nom, Prenom, Email, MotDePasse, Telephone, Role, NumeroTVA) 
            VALUES (@Nom, @Prenom, @Email, @MotDePasse, @Telephone, @Role, @NumeroTVA)";
        using var conn = CreateConnection();
        conn.Execute(sql, user);
    }
}