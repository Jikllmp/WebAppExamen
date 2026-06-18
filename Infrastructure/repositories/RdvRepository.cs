using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class RdvRepository : IRdvRepository
{
    private readonly string _connectionString;

    public RdvRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string manquante");
    }

    private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

    public void Add(int particulierId, int annonceId, DateTime dateRdv)
    {
        const string sql = @"INSERT INTO RendezVous 
            (DateRdv, Statut, ParticulierId, AnnonceId) 
            VALUES (@DateRdv, 'EnAttente', @ParticulierId, @AnnonceId)";
        using var conn = CreateConnection();
        conn.Execute(sql, new { DateRdv = dateRdv, ParticulierId = particulierId, AnnonceId = annonceId });
    }

    public void Delete(int id)
    {
        const string sql = "DELETE FROM RendezVous WHERE Id = @Id";
        using var conn = CreateConnection();
        conn.Execute(sql, new { Id = id });
    }

    public IEnumerable<RendezVous> GetByParticulier(int particulierId)
    {
        const string sql = "SELECT * FROM RendezVous WHERE ParticulierId = @ParticulierId";
        using var conn = CreateConnection();
        return conn.Query<RendezVous>(sql, new { ParticulierId = particulierId });
    }

    public IEnumerable<RendezVous> GetByAgence(int agenceId)
    {
        const string sql = @"SELECT r.* FROM RendezVous r
            INNER JOIN Annonce a ON r.AnnonceId = a.Id
            WHERE a.AgenceId = @AgenceId";
        using var conn = CreateConnection();
        return conn.Query<RendezVous>(sql, new { AgenceId = agenceId });
    }
}