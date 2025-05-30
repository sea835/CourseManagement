using CourseManagement.Models;

namespace CourseManagement.Repository;

public class ChapterRepository: GenericRepository<Chapter>
{
    public ChapterRepository(AppDbContext context) : base(context) { }
}