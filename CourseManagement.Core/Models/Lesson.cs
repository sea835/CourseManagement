namespace CourseManagement.Core.Models;

public class Lesson : BaseModel
{
    public string LessonId { get; set; }
    public string ChapterId { get; set; }
    public string Title { get; set; }
    public int? OrderNumber { get; set; }
    public string LessonType { get; set; }
    public int? Duration { get; set; }
    public bool IsPreviewable { get; set; }

    public Chapter Chapter { get; set; }
    public Content Content { get; set; }
    public Video Video { get; set; }
    public Document Document { get; set; }
}
