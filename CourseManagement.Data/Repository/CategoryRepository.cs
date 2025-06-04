using CourseManagement.Core.Interfaces.IRepository;
using CourseManagement.Core.Models;
using CourseManagement.Data.DbContext;

namespace CourseManagement.Data.Repository;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }

    public override Task Delete(string id)
    {
        var category = context.Categories.Find(id);
        if (category == null)
           throw new Exception("Category not found");
        category.SoftDelete();
        return Task.CompletedTask;
    }
}