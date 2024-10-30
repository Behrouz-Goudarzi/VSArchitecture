namespace ProductManagement.Application.Queries.GetCategoryById
{
    internal class GetCategoryByIdResponse
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}