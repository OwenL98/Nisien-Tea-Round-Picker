using Application.CommandHandlers;
using Application.Services;

namespace Api;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCommandHandlers(
        this IServiceCollection services)
    {
        services.AddTransient<IGenerateNextRoundCommandHandler, GenerateNextRoundCommandHandler>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<INameSelectorService, NameSelectorService>();

        return services;
    }
}