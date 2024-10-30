using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ProductManagement.Application.Queries.GetProducts;

internal static class GetProductsEndpoint
{
    internal static void UseGetProductsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/Products", async ( ISender sender) =>
        {
            var response = await sender.Send(new GetProductsRequest());
            return Results.Ok(response);
        }).WithName("GetProducts").WithOpenApi();
    }
}
