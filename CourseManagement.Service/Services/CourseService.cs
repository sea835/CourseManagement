using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.Models;
using CourseManagement.Core.Utilities;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;

namespace CourseManagement.Service.Services;
public class CourseService : ICourseService
{
    private readonly IUnitOfWork unitOfWork;
    public CourseService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public ResultViewModel GetAllCourse()
    {
        try
        {
            var courses = unitOfWork.Course.GetAll().Where(c => c.IsDeleted == false);
            return ResultViewModel.Success("Get All Course Success!",courses);
        }
        catch (Exception ex){ 
            return ResultViewModel.FailException(ex);
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
            course.SetUpdated();
            unitOfWork.Course.Update(course);
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
}