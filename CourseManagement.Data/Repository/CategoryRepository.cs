using CourseManagement.Core.Interfaces.IRepository;
using CourseManagement.Core.Models;
using CourseManagement.Data.DbContext;

namespace CourseManagement.Data.Repository;

public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }
}