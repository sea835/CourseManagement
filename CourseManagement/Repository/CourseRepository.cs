using CourseManagement.Models;
using PersonalExpenseTracker.ViewModels;

namespace CourseManagement.Repository;

public class CourseRepository: GenericRepository<Course>
{
    public CourseRepository(AppDbContext context) : base(context) {}

    public override ResultViewModel GetAll()
    {
        try
        {
            var courses = (from course in context.Courses
                            join category in context.Categories on course.CategoryId equals category.CategoryId
                            where course.IsDeleted == false
                            select new CourseViewModel
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
                                Category = category.Name
                            }).ToList();

            return ResultViewModel.Success(null, courses);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}