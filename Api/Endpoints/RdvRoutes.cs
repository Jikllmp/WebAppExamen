using Core.UseCases.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Endpoints;

public static class RdvRoutes
{
    public static WebApplication AddRdvRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/rdv").WithTags("RendezVous");

        group.MapPost("", ([FromBody] CreateRdvRequest request, IRdvUseCases useCases, HttpContext httpContext) =>
        {
            var particulierId = int.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            useCases.CreateRdv(particulierId, request.AnnonceId, request.DateRdv);
            return Results.Created();
        })
        .RequireAuthorization("ParticulierOnly")
        .WithName("CreateRdv");

        group.MapDelete("/{id}", (int id, IRdvUseCases useCases) =>
        {
            useCases.DeleteRdv(id);
            return Results.Ok();
        })
        .RequireAuthorization()
        .WithName("CancelRdv");

        group.MapGet("/mes-rdv", (IRdvUseCases useCases, HttpContext httpContext) =>
        {
            var userId = int.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var rdvs = useCases.GetAllRdv(userId);
            return Results.Ok(rdvs);
        })
        .RequireAuthorization("ParticulierOnly")
        .WithName("GetMesRdv");

        group.MapGet("/agence", (IRdvUseCases useCases, HttpContext httpContext) =>
        {
            var agenceId = int.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var rdvs = useCases.GetAllRdv2(agenceId);
            return Results.Ok(rdvs);
        })
        .RequireAuthorization("AgenceOnly")
        .WithName("GetRdvAgence");

        return app;
    }
}

public record CreateRdvRequest(int AnnonceId, DateTime DateRdv);