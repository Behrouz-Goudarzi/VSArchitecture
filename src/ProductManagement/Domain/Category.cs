using ProductManagement.ValueObjects;
using SharedKernel.SeedWork;

namespace ProductManagement.Domain;

internal class Category : AggregateRoot<CategoryId>
{
    #region Constructor
    private Category()
    {

    }
    #endregion
    #region Properties
    public string Title { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsParent { get; private set; }
    public ICollection<Category> SubCategories { get; private set; }
    public ICollection<Product> Products { get; private set; }
    #endregion

    #region static Method(s)
    public static Category CreateParentCategory(string title, string name,  string? description = null)
    {
        var category = new Category()
        {
            Title = title,
            Description = description,
            IsParent = true,

            Name=name
        };
        return category;
    }
    #endregion
    #region Method(s)
    /// <summary>
    /// add Category to Sub Categorires of Parent 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="description"></param>
    /// <returns>Sub Category</returns>
    public Category AddToSubCategories(string title,string name, string? description = null)
    {
        var subCategory = new Category()
        {
            Title = title,
            Name = name,
            Description = description,
            IsParent=false
        };

        if(SubCategories is null ) SubCategories = new List<Category>();
        SubCategories.Add(subCategory);
        return subCategory;
    }

    #endregion
}
