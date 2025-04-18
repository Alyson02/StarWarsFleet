using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit.Sdk;

namespace StarWarsFleet.Tests.Integration;

public class FactionControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task AddFaction_ShouldReturnOkAndCreatedFaction()
    {
        var model = new FactionViewModel { Name = "Nova República" };

        var response = await _client.PostAsJsonAsync("/faction", model);

        response.EnsureSuccessStatusCode();

        var faction = await response.Content.ReadFromJsonAsync<FactionEntity>();

        Assert.NotNull(faction);
        Assert.Equal("Nova República", faction!.Name);
    }

    [Fact]
    public async Task ListFactions_ShouldReturnOkWithList()
    {
        var response = await _client.GetAsync("/faction");

        response.EnsureSuccessStatusCode();

        var factions = await response.Content.ReadFromJsonAsync<List<FactionEntity>>();

        Assert.NotNull(factions);
    }

    [Fact]
    public async Task UpdateFaction_ShouldModifyExistingFaction()
    {
        var createModel = new FactionViewModel { Name = "Império Galáctico" };
        var postResponse = await _client.PostAsJsonAsync("/faction", createModel);
        var createdFaction = await postResponse.Content.ReadFromJsonAsync<FactionEntity>();

        var updateModel = new FactionViewModel { Name = "Primeira Ordem" };
        var putResponse = await _client.PutAsJsonAsync($"/faction/{createdFaction!.Id}", updateModel);

        putResponse.EnsureSuccessStatusCode();

        var updatedFaction = await putResponse.Content.ReadFromJsonAsync<FactionEntity>();
        Assert.Equal("Primeira Ordem", updatedFaction!.Name);
    }

    [Fact]
    public async Task DeleteFaction_ShouldRemoveFaction()
    {
        var model = new FactionViewModel { Name = "Separatistas" };
        var postResponse = await _client.PostAsJsonAsync("/faction", model);
        var createdFaction = await postResponse.Content.ReadFromJsonAsync<FactionEntity>();

        var deleteResponse = await _client.DeleteAsync($"/faction/{createdFaction!.Id}");

        Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

        // Confirma que foi deletado
        var getResponse = await _client.GetAsync("/faction");
        var list = await getResponse.Content.ReadFromJsonAsync<List<FactionEntity>>();
        Assert.DoesNotContain(list, f => f.Id == createdFaction.Id);
    }
}