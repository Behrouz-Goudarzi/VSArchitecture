using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ProductManagement.Infrastructure.Persistence;

internal sealed class ProductForQueryDbContext
{
    public readonly SqlConnection SqlConnection;



    public ProductForQueryDbContext(IConfiguration configuration):base()
    {
        SqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
}

