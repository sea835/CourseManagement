using CourseManagement.Models;
using CourseManagement.ViewModels;

namespace CourseManagement.IServices;

public interface ILessonService
{
    public ResultViewModel DeleteLesson(string id);
    public ResultViewModel GetLessonById(string id);
    public ResultViewModel UpdateLesson(Lesson lesson);
    public ResultViewModel CreateLesson(Lesson lesson);
    public ResultViewModel GetAllLessons();
}