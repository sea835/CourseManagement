namespace CourseManagement.IServices;

using CourseManagement.Models;
using CourseManagement.ViewModels;

public interface ICourseService
{
    public ResultViewModel GetAllCourse();
    public CourseViewModel GetCourseById(string id);
    public ResultViewModel CreateCourse(Course course);
    public ResultViewModel UpdateCourse(CourseViewModel course);
    public ResultViewModel DeleteCourse(string id);

}