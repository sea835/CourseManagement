using CourseManagement.Models;

namespace CourseManagement.Repository;

public class LessonRepository: GenericRepository<Lesson>
{
    public LessonRepository(AppDbContext context) : base(context) { }
}