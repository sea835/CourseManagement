using CourseManagement.IRepository;
using CourseManagement.Models;

namespace CourseManagement.Repository;

public class ContentRepository: GenericRepository<Content>, IContentRepository
{
    public ContentRepository(AppDbContext context) : base(context) { }
}