using CourseManagement.Core.Models;

namespace CourseManagement.Core.ViewModels;

public class LessonViewModel: Lesson
{
    public string CourseTitle { get; set; }
    public string ChapterTitle { get; set; }
}