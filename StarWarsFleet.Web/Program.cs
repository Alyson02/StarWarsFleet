
using StarWarsFleet.Application.Extensions;
using StarWarsFleet.Infrastructure.Extensions;
using StarWarsFleet.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

builder.Services.ConfigureDbContext(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty);

builder.Services
.ConfigureMassTransit(builder.Configuration.GetSection("RabbitMQ:Username").Value ?? string.Empty,
 builder.Configuration.GetSection("RabbitMQ:Password").Value ?? string.Empty);

builder.Services.ConfigureDocumentation();

builder.Services.AddHandlers();

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

public abstract partial class Program { }