namespace CourseManagement.Models;

public class Content : BaseModel
{
    public string ContentId { get; set; }
    public string LessonId { get; set; }
    public string MainContent { get; set; }
    public string Summary { get; set; }

    public Lesson Lesson { get; set; }
}
