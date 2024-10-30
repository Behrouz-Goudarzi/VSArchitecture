using ProductManagement.Domain;

namespace ProductManagement.Application.Queries.GetCategories;

internal class GetCategoriesResponse
{

    public IList<CategoryDto> Parents { get; set; }=new List<CategoryDto>();
    public int TotalCount { get; set; }

    public class CategoryDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IList<CategoryDto>? SubCategories { get; set; } = new List<CategoryDto>();
    }
}