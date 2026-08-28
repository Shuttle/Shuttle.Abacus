using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Shuttle.Access.AspNetCore;

namespace Shuttle.Abacus.WebApi.Endpoints;

public static class ServerEndpoints
{
    public static WebApplication MapServerEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var apiVersion1 = new ApiVersion(1, 0);

        app.MapGet("/v{version:apiVersion}/server/configuration", () =>
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0.0";

                return Results.Ok(new { Version = version });
            })
            .WithTags("Server").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequireSession();

        return app;
    }
}
