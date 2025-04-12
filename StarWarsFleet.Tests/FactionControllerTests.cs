using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace StarWarsFleet.Tests;

public class FactionControllerTests
{
    private readonly HttpClient _client;

    public FactionControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<StarWarsDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Adiciona o contexto em memória
                services.AddDbContext<StarWarsDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });
            });
        }).CreateClient();
    }

    [Fact]
    public async Task Should_Create_And_List_Faction()
    {
        // Arrange
        var model = new FactionViewModel { Name = "Império Galáctico" };

        // Act
        var postResponse = await _client.PostAsJsonAsync("/faction", model);
        postResponse.EnsureSuccessStatusCode();

        var createdFaction = await postResponse.Content.ReadFromJsonAsync<FactionEntity>();

        var listResponse = await _client.GetAsync("/faction");
        listResponse.EnsureSuccessStatusCode();

        var factions = await listResponse.Content.ReadFromJsonAsync<List<FactionEntity>>();

        // Assert
        Assert.NotNull(createdFaction);
        Assert.Contains(factions!, f => f.Id == createdFaction!.Id);
    }
}