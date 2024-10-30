using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Application.Commands.CreateCategory;

internal sealed class CreateCategoryRequest : IRequest<CreateCategoryResponse>
{

    public string? Title { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
    public string FileName { get; set; }
    public string? FileBase64 { get; set; }
    [NotMapped]
    public IFormFile? File
    {
        get
        {
            if (string.IsNullOrWhiteSpace(FileBase64))
            {
                return null;
            }
            byte[] bytes = Convert.FromBase64String(FileBase64);
            MemoryStream stream = new MemoryStream(bytes);

            IFormFile file = new FormFile(stream, 0, bytes.Length, "temp", FileName);
            return file;
        }
    }

    public long? ParentId { get; set; }
}

internal sealed class Handler : IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
{
    private readonly ProductDbContext _dbContext;
    private readonly AppSettings _appSettins;
    public Handler(ProductDbContext dbContext, AppSettings appSettins)
    {
        _dbContext = dbContext;
        _appSettins = appSettins;
    }
    public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {

        if (request is null) throw new ArgumentNullException(nameof(request));

        if (request.ParentId is not null)
            return await AddToSubCategories(request, cancellationToken);

        return await CreateParentCategory(request, cancellationToken);

    }

    private async Task<CreateCategoryResponse> CreateParentCategory(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
      
        try
        {
            
            var category = Category.CreateParentCategory(request.Title, request.Name,  request.Description);
            _dbContext.Add(category);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new(category.Id.Value, category.Title);
        }
        catch (Exception ex)
        {

            throw new($"error on upload (Err: 3): {ex.Message}");

        }
    }

    private async Task<CreateCategoryResponse> AddToSubCategories(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        if (request.ParentId <= 0) throw new Exception("InValid Parent");
        var parent = await _dbContext.Categories.FirstOrDefaultAsync(m => m.Id == CategoryId.From(request.ParentId.Value));
        if (parent is null) throw new Exception("InValid Parent");
        try
        {
 
            var result = parent.AddToSubCategories(request.Title,
                request.Name,  request.Description);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new(result.Id.Value, result.Title);
        }
        catch (Exception ex)
        {


            throw new($"error on upload (Err: 3): {ex.Message}");

        }

    }
}
