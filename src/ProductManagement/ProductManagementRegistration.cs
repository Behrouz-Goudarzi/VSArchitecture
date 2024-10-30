using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application;
using ProductManagement.Application.Commands.CreateCategory;
using ProductManagement.Application.Commands.CreateProduct;
using ProductManagement.Application.Queries.GetCategories;
using ProductManagement.Application.Queries.GetCategoryById;
using ProductManagement.Application.Queries.GetProducts;
using ProductManagement.Application.Queries.ExistsCategory;
using ProductManagement.Infrastructure.Persistence;

namespace ProductManagement;

public static class ProductManagementRegistration
{
    public static IServiceCollection AddProductManagement(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddDbContext<ProductDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName)));
        services.AddTransient<ProductForQueryDbContext>();
        services.AddSingleton<AppSettings>();
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ProductManagementRegistration).Assembly);
            //options.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
            //options.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            //options.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            //options.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
        });

        return services;
    }
    public static void UseProductManagementEndpoints(this IEndpointRouteBuilder app)
    {
        app.UseCreateCategoryEndpoint();
        app.UseCreateProductEndpoint();
        app.UseGetProductsEndpoint();
        app.UseGetCategoriesEndpoint();
        app.UseGetCategoryByIdEndpoint();
        app.UseExistsCategoryEndpoint();
    }
}
