using MassTransit;
using Microsoft.EntityFrameworkCore;
using FactionHandler = StarWarsFleet.Application.Factions.UseCases.Create.Handler;
using StarWarsFleet.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        x =>
        {
            x.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddDbContext<StarWarsDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMassTransit(x => {
    x.UsingRabbitMq((ctx, cfg) => {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

ConfigureServices(builder.Services);


var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();
return;

void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<FactionHandler>();
}

public partial class Program { }