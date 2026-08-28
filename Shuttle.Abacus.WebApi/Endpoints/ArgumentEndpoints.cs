using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;
using Shuttle.Abacus.Application;
using Shuttle.Access.AspNetCore;

namespace Shuttle.Abacus.WebApi.Endpoints;

public static class ArgumentEndpoints
{
    private static Contracts.v1.Argument Map(Query.Argument argument)
    {
        return new()
        {
            Id = argument.Id,
            Name = argument.Name,
            DataTypeName = argument.DataTypeName
        };
    }

    public static WebApplication MapArgumentEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var apiVersion1 = new ApiVersion(1, 0);

        app.MapPost("/v{version:apiVersion}/arguments/search", PostSearch)
            .WithTags("Arguments").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Arguments);

        app.MapPost("/v{version:apiVersion}/arguments", Post)
            .WithTags("Arguments").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Arguments);

        app.MapGet("/v{version:apiVersion}/arguments/{id:Guid}", Get)
            .WithTags("Arguments").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Arguments);

        app.MapPatch("/v{version:apiVersion}/arguments/{id:Guid}/name", PatchName)
            .WithTags("Arguments").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Arguments);

        app.MapDelete("/v{version:apiVersion}/arguments/{id:Guid}", Delete)
            .WithTags("Arguments").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Arguments);

        app.MapGet("/v{version:apiVersion}/arguments/{id:Guid}/values", Values)
            .WithTags("Arguments").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Arguments);

        app.MapPost("/v{version:apiVersion}/arguments/{id:Guid}/values", PostValue)
            .WithTags("Arguments").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Arguments);

        app.MapDelete("/v{version:apiVersion}/arguments/{id:Guid}/values", DeleteValue)
            .WithTags("Arguments").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Arguments);

        return app;
    }

    private static async Task<IResult> PostSearch(IArgumentQuery argumentQuery, [FromBody] Contracts.v1.Argument.Specification specification)
    {
        var search = new Query.Argument.Specification();

        if (!string.IsNullOrWhiteSpace(specification.NameMatch))
        {
            search.WithNameMatch(specification.NameMatch);
        }

        search.AddIds(specification.Ids);

        return Results.Ok((await argumentQuery.SearchAsync(search)).Select(Map).ToList());
    }

    private static async Task<IResult> Post([FromBody] Contracts.v1.Argument message, MessageDispatcher messageDispatcher)
    {
        var id = message.Id ?? Guid.NewGuid();

        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterArgument { Id = id, Name = message.Name, DataTypeName = message.DataTypeName },
            () => new RegisterArgument(id, message.Name, message.DataTypeName));

        return Results.Accepted();
    }

    private static async Task<IResult> Get(Guid id, IArgumentQuery argumentQuery)
    {
        var argument = (await argumentQuery.SearchAsync(new Query.Argument.Specification().AddId(id))).SingleOrDefault();

        return argument != null ? Results.Ok(Map(argument)) : Results.NotFound();
    }

    private static async Task<IResult> PatchName(Guid id, [FromBody] Contracts.v1.RenameArgument message, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RenameArgument { ArgumentId = id, Name = message.Name },
            () => new RenameArgument(id, message.Name));

        return Results.Accepted();
    }

    private static async Task<IResult> Delete(Guid id, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RemoveArgument { ArgumentId = id },
            () => new RemoveArgument(id));

        return Results.Accepted();
    }

    private static async Task<IResult> Values(Guid id, IArgumentQuery argumentQuery)
    {
        return Results.Ok((await argumentQuery.ValuesAsync(id)).Select(value => value.ArgumentValue).ToList());
    }

    private static async Task<IResult> PostValue(Guid id, [FromBody] Contracts.v1.ArgumentValue message, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterArgumentValue { ArgumentId = id, Value = message.Value },
            () => new RegisterArgumentValue(id, message.Value));

        return Results.Accepted();
    }

    private static async Task<IResult> DeleteValue(Guid id, [FromBody] Contracts.v1.ArgumentValue message, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RemoveArgumentValue { ArgumentId = id, Value = message.Value },
            () => new RemoveArgumentValue(id, message.Value));

        return Results.Accepted();
    }
}
