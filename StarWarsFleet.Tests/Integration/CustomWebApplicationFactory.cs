using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StarWarsFleet.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove o contexto anterior
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<StarWarsDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            // Remove o próprio contexto, caso registrado diretamente
            var dbContext = services.SingleOrDefault(
                d => d.ServiceType == typeof(StarWarsDbContext));

            if (dbContext != null)
                services.Remove(dbContext);

            // Cria novo provedor isolado
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Registra com InMemory e provider isolado
            services.AddDbContext<StarWarsDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                options.UseInternalServiceProvider(serviceProvider); // <- ESSENCIAL!
            });

            // Garante que o banco foi criado
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<StarWarsDbContext>();
            db.Database.EnsureCreated();
        });
    }
}