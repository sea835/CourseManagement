using CourseManagement.Core.Models;
using CourseManagement.Core.Utilities;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;
using CourseManagement.Service.IServices;

namespace CourseManagement.Service.Services;
public class CourseService : ICourseService
{
    private readonly IUnitOfWork unitOfWork;
    public CourseService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public IEnumerable<CourseViewModel> GetAllCourse()
    {
        try
        {
            var courses = unitOfWork.Course.GetAll().Where(c => c.IsDeleted == false).ToList();
            var categories = unitOfWork.Category.GetAll().Where(c => c.IsDeleted == false).ToList();
            var courseViewModels = from course in courses
                                   join category in categories on course.CategoryId equals category.CategoryId
                                   select new CourseViewModel
                                   {
                                       CourseId = course.CourseId,
                                       Title = course.Title,
                                       Description = course.Description,
                                       FullDescription = course.FullDescription,
                                       Level = course.Level,
                                       Duration = course.Duration,
                                       Language = course.Language,
                                       ThumbnailUrl = course.ThumbnailUrl,
                                       Price = course.Price,
                                       IsFree = course.IsFree,
                                       AuthorName = course.AuthorName,
                                       CategoryId = course.CategoryId,
                                       CategoryName = category.Name
                                   };
            return courseViewModels;
        }
        catch (Exception ex){ 
            throw new Exception("Get All Course Failed", ex);
        }
    }

    public CourseViewModel GetCourseById(string id)
    {
        try
        {
            var query = unitOfWork.Course.BuildQuery(c => c.CourseId == id).FirstOrDefault();
            var category = unitOfWork.Category.BuildQuery(c => c.CategoryId == query.CategoryId).FirstOrDefault();
            var course = new CourseViewModel()
            {
                CourseId = query.CourseId,
                Title = query.Title,
                Description = query.Description,
                FullDescription = query.FullDescription,
                Level = query.Level,
                Duration = query.Duration,
                Language = query.Language,
                ThumbnailUrl = query.ThumbnailUrl,
                Price = query.Price,
                IsFree = query.IsFree,
                AuthorName = query.AuthorName,
                CategoryId = query.CategoryId,
                CategoryName = category.Name,
            };
            return course;
        }
        catch (Exception ex)
        {
            throw new Exception("Get Course By Id Failed", ex);
        }
    }

    public ResultViewModel CreateCourse(Course course)
    {
        try
        {
            course.CourseId = IdGenerator.GenerateCourseId();
            unitOfWork.Course.Add(course);
            unitOfWork.SaveChange();
            return ResultViewModel.Success("Create New Course Success");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public ResultViewModel DeleteCourse(string id)
    {
        try
        {
            unitOfWork.Course.Delete(id);
            unitOfWork.SaveChange();
            return ResultViewModel.Success("Delete Course Success");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public ResultViewModel UpdateCourse(CourseViewModel course)
    {
        try
        {
            var existingCourse = unitOfWork.Course.BuildQuery(c=> c.CourseId == course.CourseId).FirstOrDefault();
            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            existingCourse.FullDescription = course.FullDescription;
            existingCourse.Level = course.Level;
            existingCourse.Duration = course.Duration;
            existingCourse.Language = course.Language;
            existingCourse.ThumbnailUrl = course.ThumbnailUrl;
            existingCourse.Price = course.Price;
            existingCourse.IsFree = course.IsFree;
            existingCourse.AuthorName = course.AuthorName;
            existingCourse.CategoryId = course.CategoryId;
            existingCourse.SetUpdated();
            unitOfWork.Course.Update(existingCourse);
            unitOfWork.SaveChange();
            return ResultViewModel.Success("Update Course Success");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel GetAllCoursesSelect2(string search)
    {
        try
        {
            var courses = unitOfWork.Course.GetAll()
                .Where(c => c.IsDeleted == false && (string.IsNullOrEmpty(search) || c.Title.Contains(search, StringComparison.OrdinalIgnoreCase)))
                .Select(c => new Select2ViewModel
                {
                    Id = c.CourseId,
                    Text = c.Title
                });
            return ResultViewModel.Success("Get All Courses Select2 Success", courses);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel GetAllCoursesByCategoryId(string categoryId)
    {
        try
        {
            var courses = unitOfWork.Course.GetAll()
                .Where(c => c.IsDeleted == false && c.CategoryId == categoryId)
                .Select(c => new Select2ViewModel
                {
                    Id = c.CourseId,
                    Text = c.Title
                });
            return ResultViewModel.Success("Get All Courses By Category Id Success", courses);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public CourseViewModel GetCourseByLessonId(string lessonId)
    {
        var lesson = unitOfWork.Lesson.BuildQuery(l => l.LessonId == lessonId).FirstOrDefault();
        var chapter = unitOfWork.Chapter.BuildQuery(c => c.ChapterId == lesson.ChapterId).FirstOrDefault();
        var course = unitOfWork.Course.BuildQuery(c => c.CourseId == chapter.CourseId).FirstOrDefault();

        return new CourseViewModel
        {
            CourseId = course.CourseId,
            Title = course.Title,
            Description = course.Description,
            FullDescription = course.FullDescription,
            Level = course.Level,
            Duration = course.Duration,
            Language = course.Language,
            ThumbnailUrl = course.ThumbnailUrl,
            Price = course.Price,
            IsFree = course.IsFree,
            AuthorName = course.AuthorName,
            CategoryId = course.CategoryId,
            CategoryName = unitOfWork.Category.BuildQuery(c => c.CategoryId == course.CategoryId).FirstOrDefault()?.Name
        };

    }
}