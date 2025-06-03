using CourseManagement.IRepository;
using CourseManagement.Models;

namespace CourseManagement.Repository;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(AppDbContext context) : base(context)
    {
    }

    public override IEnumerable<Course> GetAll()
    {
        var courses = (from course in context.Courses
            join category in context.Categories on course.CategoryId equals category.CategoryId
            where course.IsDeleted == false
            select new Course
            {
                CourseId = course.CourseId,
                Title = course.Title,
                Description = course.Description,
                FullDescription = course.FullDescription,
                Level = course.Level,
                Price = course.Price,
                Language = course.Language,
                Duration = course.Duration,
                AuthorName = course.AuthorName,
                Category = category,
            }).AsEnumerable();

        return courses;
    }

    public override Task Delete(string id)
    {
        var course = context.Courses.Find(id);
        if (course == null)
            return Task.CompletedTask;

        course.IsDeleted = true;
        return Update(course);
    }
}