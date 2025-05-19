using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarWarsFleet.Infrastructure.Data;

namespace StarWarsFleet.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static void ConfigureDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<StarWarsDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}