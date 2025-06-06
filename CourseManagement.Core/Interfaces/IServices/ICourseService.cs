using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;

namespace CourseManagement.Core.Interfaces.IServices;

public interface ICourseService
{
    public IEnumerable<CourseViewModel> GetAllCourse();
    public CourseViewModel GetCourseById(string id);
    public ResultViewModel CreateCourse(Course course);
    public ResultViewModel UpdateCourse(CourseViewModel course);
    public ResultViewModel DeleteCourse(string id);
    public ResultViewModel GetAllCoursesSelect2(string search);
    public ResultViewModel GetAllCoursesByCategoryId(string categoryId);
    public CourseViewModel GetCourseByLessonId(string lessonId);

}