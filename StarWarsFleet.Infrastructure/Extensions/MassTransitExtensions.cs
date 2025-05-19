using Microsoft.Extensions.DependencyInjection;
using MassTransit;

namespace StarWarsFleet.Infrastructure.Extensions;

public static class MassTransitExtensions
{
    public static void ConfigureMassTransit(this IServiceCollection services, string username, string password)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username(username);
                    h.Password(password);
                });
            });
        });
    }
}