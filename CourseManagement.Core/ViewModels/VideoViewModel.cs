using CourseManagement.Core.Models;

namespace CourseManagement.Core.ViewModels;

public class VideoViewModel: Video
{
    public string? CourseTitle { get; set; }
    public string? LessonTitle { get; set; }
    public string? ChapterTitle { get; set; }
}