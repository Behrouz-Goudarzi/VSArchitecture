using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ProductManagement.Application.Queries.GetCategories;

internal static class GetCategoriesEndpoint
{
    internal static void UseGetCategoriesEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/Categories", async ( ISender sender) =>
        {
            var response = await sender.Send(new GetCategoriesRequest());
            return Results.Ok(response);
        }).WithName("GetCategories").WithOpenApi();
    }
}
