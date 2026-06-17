using Core.UseCases;
using Core.UseCases.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceExtension
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddTransient<IUtilisateurUseCases, UtilisateurUseCases>();
        services.AddTransient<IAnnonceUseCases, AnnonceUseCases>();
        services.AddTransient<IRdvUseCases, RdvUseCases>();
        services.AddTransient<IFavoriUseCases, FavoriUseCases>();
        return services;
    }
}