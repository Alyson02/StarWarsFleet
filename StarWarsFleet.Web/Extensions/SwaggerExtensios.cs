namespace StarWarsFleet.Web.Extensions;

public static class SwaggerExtensions
{
    public static void ConfigureDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}