namespace Done.Api.Endpoints;

public static class EndpointsExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGroup("api/users")
            .WithOpenApi()
            .MapUserEndpoints();

        app.MapGroup("api/todos")
            .WithOpenApi()
            .MapToDoEndpoints();

        app.MapGroup("api/todo-lists")
            .WithOpenApi()
            .MapToDoListEndpoints();

        return app;
    }
}