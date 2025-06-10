namespace CourseManagement.Core.ViewModels;

public class LessonResponseModel
{
    public string LessonId { get; set; }
    public string ChapterId { get; set; }
    public string Title { get; set; }
    public int? OrderNumber { get; set; }
    public string LessonType { get; set; }
    public int? Duration { get; set; }
    public bool IsPreviewable { get; set; }
    public string ChapterTitle { get; set; }
    public string CourseTitle { get; set; }
}
