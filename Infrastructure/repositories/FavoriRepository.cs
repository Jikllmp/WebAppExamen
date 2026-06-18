using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class FavoriRepository : IFavoriRepository
{
    private readonly string _connectionString;

    public FavoriRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string manquante");
    }

    private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

    public void Add(int userId, int annonceId)
    {
        const string sql = "INSERT INTO Favori (UtilisateurId, AnnonceId, DateAjout) VALUES (@UserId, @AnnonceId, GETDATE())";
        using var conn = CreateConnection();
        conn.Execute(sql, new { UserId = userId, AnnonceId = annonceId });
    }

    public void Delete(int userId, int annonceId)
    {
        const string sql = "DELETE FROM Favori WHERE UtilisateurId = @UserId AND AnnonceId = @AnnonceId";
        using var conn = CreateConnection();
        conn.Execute(sql, new { UserId = userId, AnnonceId = annonceId });
    }

    public IEnumerable<Favori> GetByUser(int userId)
    {
        const string sql = "SELECT * FROM Favori WHERE UtilisateurId = @UserId";
        using var conn = CreateConnection();
        return conn.Query<Favori>(sql, new { UserId = userId });
    }
}