using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ProductManagement.Application.Queries.ExistsCategory;

internal static class ExistsCategoryEndpoint
{
    internal static void UseExistsCategoryEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapMethods("api/categories/{nameOrTitle}",new[] { "HEAD"}, async (string? nameOrTitle, ISender sender,CancellationToken cancellationToken) =>
        {
            var response = await sender.Send( new ExistsCategoryRequest(nameOrTitle),cancellationToken);
            if (response)
            {
                return Results.Ok();

            }
            return Results.NotFound();

        }).WithName("ExistsCategory").WithOpenApi();
    }
}
