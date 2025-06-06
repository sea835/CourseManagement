using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;

namespace CourseManagement.Core.Interfaces.IServices;

public interface ILessonService
{
    public ResultViewModel DeleteLesson(string id);
    public LessonViewModel GetLessonById(string id);
    public ResultViewModel UpdateLesson(Lesson lesson);
    public ResultViewModel CreateLesson(Lesson lesson);
    public ResultViewModel GetAllLessons();
}