using CourseManagement.IRepository;
using CourseManagement.Models;

namespace CourseManagement.Repository;

public class LessonRepository: GenericRepository<Lesson>, ILessonRepository
{
    public LessonRepository(AppDbContext context) : base(context) { }
}