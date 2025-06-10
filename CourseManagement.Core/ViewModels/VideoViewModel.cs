using CourseManagement.Core.Models;
using Microsoft.AspNetCore.Http;

namespace CourseManagement.Core.ViewModels;

public class VideoViewModel: Video
{
    public string? CourseTitle { get; set; }
    public string? LessonTitle { get; set; }
    public string? ChapterTitle { get; set; }
    public IFormFile VideoFile { get; set; }
}