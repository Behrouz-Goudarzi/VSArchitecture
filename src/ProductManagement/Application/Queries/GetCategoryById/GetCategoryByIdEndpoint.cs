using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ProductManagement.Application.Queries.GetCategoryById;

internal static class GetCategoryByIdEndpoint
{
    internal static void UseGetCategoryByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/Categories/{id:long}", async (long id, ISender sender) =>
        {
            var response = await sender.Send(new GetCategoryByIdRequest(id));
            return Results.Ok(response);
        }).WithName("GetCategoryById").WithOpenApi();
    }
}
