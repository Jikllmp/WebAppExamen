using Core.Models;
using Core.UseCases.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Endpoints;

public static class AnnonceRoutes
{
    public static WebApplication AddAnnonceRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/annonces").WithTags("Annonces");

        group.MapGet("", (IAnnonceUseCases useCases) =>
        {
            var annonces = useCases.GetAllAnnonce();
            return Results.Ok(annonces);
        })
        .AllowAnonymous()
        .WithName("GetAllAnnonces");

        group.MapGet("/{id}", (int id, IAnnonceUseCases useCases) =>
        {
            var annonce = useCases.GetAnnonceById(id);
            return annonce == null ? Results.NotFound() : Results.Ok(annonce);
        })
        .AllowAnonymous()
        .WithName("GetAnnonceById");

        group.MapPost("", ([FromBody] Annonce annonce, IAnnonceUseCases useCases, HttpContext httpContext) =>
        {
            var agenceId = int.Parse(httpContext.User.FindFirst("nameid")!.Value);
            useCases.CreateAnnonce(annonce, agenceId, annonce.Commodites.Select(c => c.Id));
            return Results.Created();
        })
        .RequireAuthorization("AgenceOnly")
        .WithName("CreateAnnonce");

        group.MapPut("/{id}", (int id, [FromBody] Annonce annonce, IAnnonceUseCases useCases, HttpContext httpContext) =>
        {
            var agenceId = int.Parse(httpContext.User.FindFirst("nameid")!.Value);
            annonce.Id = id;
            useCases.UpdateAnnonce(annonce, agenceId, annonce.Commodites.Select(c => c.Id));
            return Results.Ok();
        })
        .RequireAuthorization("AgenceOnly")
        .WithName("UpdateAnnonce");

        group.MapDelete("/{id}", (int id, IAnnonceUseCases useCases, HttpContext httpContext) =>
        {
            var agenceId = int.Parse(httpContext.User.FindFirst("nameid")!.Value);
            useCases.DeleteAnnonce(id, agenceId);
            return Results.Ok();
        })
        .RequireAuthorization("AgenceOnly")
        .WithName("DeleteAnnonce");

        group.MapGet("/regions", (IAnnonceUseCases useCases) => Results.Ok(useCases.GetRegion()))
        .AllowAnonymous()
        .WithName("GetRegions");

        group.MapGet("/types-bien", (IAnnonceUseCases useCases) => Results.Ok(useCases.GetTypeBien()))
        .AllowAnonymous()
        .WithName("GetTypesBien");

        group.MapGet("/commodites", (IAnnonceUseCases useCases) => Results.Ok(useCases.GetCommodites()))
        .AllowAnonymous()
        .WithName("GetCommodites");

        return app;
    }
}