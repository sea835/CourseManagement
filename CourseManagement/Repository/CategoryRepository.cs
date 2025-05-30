using CourseManagement.Models;

namespace CourseManagement.Repository;

public class CategoryRepository: GenericRepository<Category>
{
    public CategoryRepository(AppDbContext context) : base(context) { }
}