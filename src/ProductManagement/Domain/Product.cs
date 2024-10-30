using SharedKernel.ValueObjects;
using SharedKernel.SeedWork;

namespace ProductManagement.Domain;

internal class Product : AggregateRoot<ProductId>
{
    #region Constructor
    private Product()
    {

    }
    #endregion

    #region Properties
    public string Title { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Category Category { get; private set; }


    #endregion

    #region Method(s)
    public static Product Create(string title, string? description, Category category)
    {
        title = title.Trim();
        description = description?.Trim();
        Product product = new Product()
        {
            Description = description,
            Title = title,
            Category = category
        };
        return product;
    }

    #endregion

}
