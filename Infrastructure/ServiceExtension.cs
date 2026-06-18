using Core.IGateways;
using Infrastructure.Gateways;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IUtilisateurRepository, UtilisateurRepository>();
        services.AddTransient<IAnnonceRepository, AnnonceRepository>();
        services.AddTransient<IRdvRepository, RdvRepository>();
        services.AddTransient<IFavoriRepository, FavoriRepository>();

        services.AddTransient<IUtilisateurGateway, UtilisateurGateway>();
        services.AddTransient<IAnnonceGateway, AnnonceGateway>();
        services.AddTransient<IRdvGateway, RdvGateway>();
        services.AddTransient<IFavoriGateway, FavoriGateway>();

        return services;
    }
}