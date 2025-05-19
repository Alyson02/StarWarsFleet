using Microsoft.Extensions.DependencyInjection;
using DeleteHandler = StarWarsFleet.Application.Factions.UseCases.Delete.Handler;
using CreateHandler = StarWarsFleet.Application.Factions.UseCases.Create.Handler;
using UpdateHandler = StarWarsFleet.Application.Factions.UseCases.Update.Handler;

namespace StarWarsFleet.Application.Extensions;

public static class HandlerExtensions
{
    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddTransient<CreateHandler>();
        services.AddTransient<DeleteHandler>();
        services.AddTransient<UpdateHandler>();
    }
}
