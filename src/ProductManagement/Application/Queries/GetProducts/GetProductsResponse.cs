
namespace ProductManagement.Application.Queries.GetProducts;

internal class GetProductsResponse
{

    public IList<ProductDto > Products { get; set; }=new List<ProductDto>();


    public class CategoryDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

    }
    public class ProductDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public CategoryDto Catrgory { get; set; } = new();


    }
}