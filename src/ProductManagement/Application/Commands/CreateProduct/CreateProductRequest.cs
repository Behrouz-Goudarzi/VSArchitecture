using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.ValueObjects;

namespace ProductManagement.Application.Commands.CreateProduct;

internal sealed record CreateProductRequest(long categoryId,string title,string name,string description):IRequest<CreateProductResponse>;

internal sealed class Handler : IRequestHandler<CreateProductRequest, CreateProductResponse>
{
    private readonly ProductDbContext _dbContext;

    public Handler(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.categoryId <= 0) throw new Exception("Invalid Category");
        var category = await _dbContext.Categories.FirstOrDefaultAsync(m => m.Id == CategoryId.From(request.categoryId), cancellationToken);
        if(category is null) throw new Exception("Invalid Category");
        var product = Product.Create(request.title, request.description, category);
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new CreateProductResponse(product.Id.Value,product.Title);
    }
}
