using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ProductManagement.Application.Commands.CreateProduct;
internal static class CreateProductEndpoint
{
    internal static void UseCreateProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/Products", async ([FromBody]CreateProductRequest request, ISender sender) =>
        {
            var response = await sender.Send(request);
            return Results.Created($"api/Products", response);
        }).WithName("CreateProduct").WithOpenApi();
    }
}