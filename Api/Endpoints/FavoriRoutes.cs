using Core.UseCases.Abstractions;
using System.Security.Claims;

namespace Api.Endpoints;

public static class FavoriRoutes
{
    public static WebApplication AddFavoriRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/favoris").WithTags("Favoris");

        group.MapPost("/{annonceId}", (int annonceId, IFavoriUseCases useCases, HttpContext httpContext) =>
        {
            var userId = int.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            useCases.AddFavori(userId, annonceId);
            return Results.Created();
        })
        .RequireAuthorization("ParticulierOnly")
        .WithName("AddFavori");

        group.MapDelete("/{annonceId}", (int annonceId, IFavoriUseCases useCases, HttpContext httpContext) =>
        {
            var userId = int.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            useCases.DeleteFavori(annonceId);
            return Results.Ok();
        })
        .RequireAuthorization("ParticulierOnly")
        .WithName("DeleteFavori");

        group.MapGet("", (IFavoriUseCases useCases, HttpContext httpContext) =>
        {
            var userId = int.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var favoris = useCases.GetFavori(userId);
            return Results.Ok(favoris);
        })
        .RequireAuthorization("ParticulierOnly")
        .WithName("GetMesFavoris");

        return app;
    }
}