using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ProductManagement.Application.Commands.CreateCategory;
internal static class CreateCategoryEndpoint
{
    internal static void UseCreateCategoryEndpoint(this IEndpointRouteBuilder app)
    {

        app.MapPost("api/Categories", async  (CreateCategoryRequest request, ISender sender,CancellationToken cancellation) =>
        {
      
            CreateCategoryResponse response = await sender.Send(request,cancellation);
            return Results.Created($"api/Categories", response); 
        }).DisableAntiforgery()
            .Produces<CreateCategoryResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest).WithSummary("لیست دسته بندی را می توانید دریافت کنید")
            .WithName("CreateCategory")
            .WithOpenApi();
    }
}