using Dapper;
using MediatR;
using ProductManagement.Infrastructure.Persistence;
using static ProductManagement.Application.Queries.GetProducts.GetProductsResponse;

namespace ProductManagement.Application.Queries.GetProducts;

internal sealed class GetProductsRequest : IRequest<GetProductsResponse>
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }

}

internal sealed class Handler : IRequestHandler<GetProductsRequest, GetProductsResponse>
{
    private ProductForQueryDbContext _dbContext;

    public Handler(ProductForQueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
    {
        var sql = $"SELECT product.Id,product.Title,product.Description,product.CategoryId,cat.Id,cat.Title,cat.Description FROM Products product " +
            $"left join Categories cat on cat.Id=product.CategoryId ";
        var response=new GetProductsResponse();
        var result = await _dbContext.SqlConnection.QueryAsync< ProductDto, CategoryDto, ProductDto >(sql, (first, sec) =>
        {
            first.Catrgory = sec;
     
            return first;
        },splitOn: "CategoryId");
        response.Products = result.ToList();
        return response;
    }
}
