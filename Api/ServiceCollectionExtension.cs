using Application.Services;
using Common.Facades;

namespace Api;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<IParticipantSelectorService, ParticipantSelectorService>();

        return services;
    }

    public static IServiceCollection AddFacades(this IServiceCollection services)
    {
        services
            .AddTransient<IRandomFacade, RandomFacade>();
        
        return services;
    }
}