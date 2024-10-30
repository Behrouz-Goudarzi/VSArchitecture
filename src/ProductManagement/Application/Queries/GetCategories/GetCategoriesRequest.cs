using Dapper;
using MediatR;
using ProductManagement.Infrastructure.Persistence;
using static ProductManagement.Application.Queries.GetCategories.GetCategoriesResponse;

namespace ProductManagement.Application.Queries.GetCategories;

internal sealed class GetCategoriesRequest : IRequest<GetCategoriesResponse>
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }

}

internal sealed class Handler : IRequestHandler<GetCategoriesRequest, GetCategoriesResponse>
{
    private ProductForQueryDbContext _dbContext;

    public Handler(ProductForQueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetCategoriesResponse> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        var sql = $"SELECT parent.Id,parent.Title,parent.Description,parent.ParentId,child.Id,child.Title,child.Description FROM Categories parent " +
            $"left join Categories child on child.parentId=parent.Id " +
            $"where parent.isparent=1 ";
        var response=new GetCategoriesResponse();
        var result = await _dbContext.SqlConnection.QueryAsync< CategoryDto, CategoryDto, CategoryDto>(sql, (first, sec) =>
        {
            first.SubCategories?.Add(sec);
            return first;
        },splitOn:"ParentId");
        response.Parents = result.ToList();
        return response;
    }
}
