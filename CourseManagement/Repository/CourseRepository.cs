using CourseManagement.Models;

namespace CourseManagement.Repository;

public class CourseRepository: GenericRepository<Course>
{
    public CourseRepository(AppDbContext context) : base(context) {}
}