using Core.Models;
using Core.UseCases.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Endpoints;

public static class UtilisateurRoutes
{
    public static WebApplication AddUtilisateurRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/utilisateurs").WithTags("Utilisateurs");

        group.MapPost("/auth", ([FromBody] LoginRequest request, IUtilisateurUseCases useCases, IConfiguration config) =>
        {
            var user = useCases.Connection(request.Email, request.Password);

            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);
            var expireMinutes = Convert.ToDouble(config["Jwt:ExpireTimeInMinutes"] ?? "60");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Results.Ok(new { token = jwt });
        })
        .AllowAnonymous()
        .WithName("Login");

        group.MapPost("/register", ([FromBody] RegisterRequest request, IUtilisateurUseCases useCases) =>
        {
            useCases.Inscrire(request.Nom, request.Prenom, request.Email,
                              request.Password, request.Telephone,
                              request.Role, request.NumeroTVA);
            return Results.Ok(new { message = "Compte créé avec succès" });
        })
        .AllowAnonymous()
        .WithName("Register");

        return app;
    }
}

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Nom, string Prenom, string Email, string Password,
                               string Telephone, string Role, string? NumeroTVA);