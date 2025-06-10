namespace CourseManagement.Core.ViewModels;

public class VideoResponseModel
{
    public string VideoId { get; set; }
    public string LessonId { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public int? Duration { get; set; }
    public string Provider { get; set; }
    public string LessonTitle { get; set; }
    public string ChapterTitle { get; set; }
    public string CourseTitle { get; set; }
}
