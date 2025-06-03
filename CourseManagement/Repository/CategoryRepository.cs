using CourseManagement.IRepository;
using CourseManagement.Models;

namespace CourseManagement.Repository;

public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }
}