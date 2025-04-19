using MassTransit;
using Microsoft.EntityFrameworkCore;
using DeleteHandler = StarWarsFleet.Application.Factions.UseCases.Delete.Handler;
using FactionHandler = StarWarsFleet.Application.Factions.UseCases.Create.Handler;
using StarWarsFleet.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

ConfigureMvc(builder.Services);

ConfigureDbContext(builder.Services);

ConfigureMassTransit(builder.Services);

ConfigureSwagger(builder.Services);

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
    services.AddTransient<DeleteHandler>();
}

void ConfigureMvc(IServiceCollection services)
{
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
}

void ConfigureDbContext(IServiceCollection services)
{
    builder.Services.AddDbContext<StarWarsDbContext>(opt => {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

void ConfigureMassTransit(IServiceCollection services)
{
    builder.Services.AddMassTransit(x => {
        x.UsingRabbitMq((ctx, cfg) => {
            cfg.Host("localhost", "/", h => {
                h.Username("guest");
                h.Password("guest");
            });
        });
    });
}

void ConfigureSwagger(IServiceCollection services)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

public abstract partial class Program { }