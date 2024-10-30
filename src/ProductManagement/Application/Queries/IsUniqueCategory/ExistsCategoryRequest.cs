using Dapper;
using MediatR;
using ProductManagement.Infrastructure.Persistence;

namespace ProductManagement.Application.Queries.ExistsCategory;

internal sealed record ExistsCategoryRequest(string? nameOrTitle) : IRequest<bool>;

internal sealed class Handler : IRequestHandler<ExistsCategoryRequest, bool>
{
    private ProductForQueryDbContext _dbContext;

    public Handler(ProductForQueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> Handle(ExistsCategoryRequest request, CancellationToken cancellationToken)
    {
        var sql = $"SELECT count(*) FROM Categories where Title=N'{request.nameOrTitle}' or Name=N'{request.nameOrTitle}'";
        var response = await _dbContext.SqlConnection.QueryFirstAsync<int>(sql);
        return response >0;
    }
}


