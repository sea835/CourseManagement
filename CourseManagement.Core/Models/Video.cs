namespace CourseManagement.Core.Models;

public class Video : BaseModel
{
    public string VideoId { get; set; }
    public string LessonId { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public int? Duration { get; set; }
    public string Provider { get; set; }

    public Lesson Lesson { get; set; }
}
