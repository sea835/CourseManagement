using CourseManagement.Core.Models;

namespace CourseManagement.Core.ViewModels;

public class DocumentViewModel : Document
{
    public string? CourseTitle { get; set; }
    public string? LessonTitle { get; set; }
    public string? ChapterTitle { get; set; }
}