using CourseManagement.Core.Interfaces.IRepository;
using CourseManagement.Core.Models;
using CourseManagement.Data.DbContext;

namespace CourseManagement.Data.Repository;

public class LessonRepository: GenericRepository<Lesson>, ILessonRepository
{
    public LessonRepository(AppDbContext context) : base(context) { }
}