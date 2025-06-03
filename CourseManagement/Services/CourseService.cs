using CourseManagement.IServices;
using CourseManagement.ViewModels;
using CourseManagement.UnitOfWork;
using CourseManagement.Models;
using CourseManagement.Utilities;

namespace CourseManagement.Services;
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
            var query = unitOfWork.Course.Find(c => c.CourseId == id).FirstOrDefault();
            var category = unitOfWork.Category.Find(c => c.CategoryId == query.CategoryId).FirstOrDefault();
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
            course.CourseId = CourseIdGenerator.GenerateCourseId();
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
}