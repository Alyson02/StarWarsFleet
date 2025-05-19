namespace StarWarsFleet.Web.Extensions;

public static class MvcExtensions
{
    public static void ConfigureMvc(this WebApplicationBuilder builder)
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
}