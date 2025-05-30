using CourseManagement.Models;

namespace CourseManagement.Repository;

public class ContentRepository: GenericRepository<Content>
{
    public ContentRepository(AppDbContext context) : base(context) { }
}