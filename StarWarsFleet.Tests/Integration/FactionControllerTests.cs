using System.Dynamic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json;
using StarWarsFleet.Application.Factions.UseCases.Update;
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
        var createModel = new Command() { Name = "Império Galáctico" };
        var postResponse = await _client.PostAsJsonAsync("/faction", createModel);
        var createdFaction = await postResponse.Content.ReadFromJsonAsync<FactionEntity>();

        var updateModel = new FactionViewModel { Name = "Primeira Ordem" };
        var putResponse = await _client.PutAsJsonAsync($"/faction/{createdFaction!.Id}", updateModel);

        putResponse.EnsureSuccessStatusCode();

        var rawContent = await putResponse.Content.ReadAsStringAsync();
        var updatedFaction = JsonConvert.DeserializeObject<dynamic>(rawContent);

        Assert.Equal("Primeira Ordem", updatedFaction!.data!.name.ToString());
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

        if(list is null) throw new NullException("list não pode ser nulo");

        Assert.DoesNotContain(list, f => f.Id == createdFaction.Id);
    }
}