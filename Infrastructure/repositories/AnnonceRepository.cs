using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class AnnonceRepository : IAnnonceRepository
{
    private readonly string _connectionString;

    public AnnonceRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string manquante");
    }

    private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

    public IEnumerable<Annonce> GetAll()
    {
        const string sql = "SELECT * FROM Annonce";
        using var conn = CreateConnection();
        return conn.Query<Annonce>(sql);
    }

    public Annonce? GetById(int id)
    {
        const string sql = "SELECT * FROM Annonce WHERE Id = @Id";
        using var conn = CreateConnection();
        return conn.QuerySingleOrDefault<Annonce>(sql, new { Id = id });
    }

    public void Add(Annonce annonce, int agenceId, IEnumerable<int> commoditeIds)
    {
        const string sql = @"INSERT INTO Annonce 
            (Titre, Description, Prix, NombrePieces, Superficie, Jardin, Garage, DatePublication, AgenceId, TypeBienId, RegionId) 
            VALUES (@Titre, @Description, @Prix, @NombrePieces, @Superficie, @Jardin, @Garage, GETDATE(), @AgenceId, @TypeBienId, @RegionId);
            SELECT SCOPE_IDENTITY();";
        using var conn = CreateConnection();
        var annonceId = conn.ExecuteScalar<int>(sql, new
        {
            annonce.Tite,
            annonce.Description,
            annonce.Prix,
            annonce.NbPieces,
            annonce.Superficie,
            annonce.Jardin,
            annonce.Garage,
            AgenceId = agenceId,
            annonce.TypeBienId,
            annonce.RegionId
        });

        foreach (var commoditeId in commoditeIds)
        {
            const string sqlCommodite = "INSERT INTO AnnonceCommodite (AnnonceId, CommoditeId) VALUES (@AnnonceId, @CommoditeId)";
            conn.Execute(sqlCommodite, new { AnnonceId = annonceId, CommoditeId = commoditeId });
        }
    }

    public void Update(Annonce annonce, IEnumerable<int> commoditeIds)
    {
        const string sql = @"UPDATE Annonce SET 
            Titre = @Titre, Description = @Description, Prix = @Prix,
            NombrePieces = @NombrePieces, Superficie = @Superficie,
            Jardin = @Jardin, Garage = @Garage,
            TypeBienId = @TypeBienId, RegionId = @RegionId
            WHERE Id = @Id";
        using var conn = CreateConnection();
        conn.Execute(sql, annonce);

        const string sqlDelete = "DELETE FROM AnnonceCommodite WHERE AnnonceId = @AnnonceId";
        conn.Execute(sqlDelete, new { AnnonceId = annonce.Id });

        foreach (var commoditeId in commoditeIds)
        {
            const string sqlCommodite = "INSERT INTO AnnonceCommodite (AnnonceId, CommoditeId) VALUES (@AnnonceId, @CommoditeId)";
            conn.Execute(sqlCommodite, new { AnnonceId = annonce.Id, CommoditeId = commoditeId });
        }
    }

    public void Delete(int id)
    {
        const string sql = "DELETE FROM Annonce WHERE Id = @Id";
        using var conn = CreateConnection();
        conn.Execute(sql, new { Id = id });
    }

    public IEnumerable<Region> GetAllRegions()
    {
        const string sql = "SELECT * FROM Region";
        using var conn = CreateConnection();
        return conn.Query<Region>(sql);
    }

    public IEnumerable<TypeB> GetAllTypesBien()
    {
        const string sql = "SELECT * FROM TypeBien";
        using var conn = CreateConnection();
        return conn.Query<TypeB>(sql);
    }

    public IEnumerable<Commodite> GetAllCommodites()
    {
        const string sql = "SELECT * FROM Commodite";
        using var conn = CreateConnection();
        return conn.Query<Commodite>(sql);
    }
}