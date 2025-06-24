using Application.CommandHandlers;
using Application.Services;
using Common.Facades;

namespace Api;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCommandHandlers(
        this IServiceCollection services)
    {
        services
            .AddTransient<IGenerateNextRoundCommandHandler, GenerateNextRoundCommandHandler>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<INameSelectorService, NameSelectorService>();

        return services;
    }

    public static IServiceCollection AddFacades(this IServiceCollection services)
    {
        services
            .AddTransient<IRandomFacade, RandomFacade>();
        
        return services;
    }
}