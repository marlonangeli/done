namespace Done.Api.Endpoints;

public static class EndpointsExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => "Hello World!");

        return app;
    }
}