using Dapper;
using MediatR;
using ProductManagement.Application.Queries.GetCategories;
using ProductManagement.Domain;
using ProductManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Queries.GetCategoryById;

internal sealed record GetCategoryByIdRequest(long categoryId) : IRequest<GetCategoryByIdResponse>;

internal sealed class Handler : IRequestHandler<GetCategoryByIdRequest, GetCategoryByIdResponse>
{
    private ProductForQueryDbContext _dbContext;

    public Handler(ProductForQueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var sql = $"SELECT Id,Title,Description FROM Categories where id={request.categoryId}";
        var response = await _dbContext.SqlConnection.QueryFirstOrDefaultAsync<GetCategoryByIdResponse>(sql);
        if (response == null) throw new InvalidDataException("Invalid Category Id");
        return response;
    }
}


