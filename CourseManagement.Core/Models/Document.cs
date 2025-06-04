namespace CourseManagement.Core.Models;

public class Document : BaseModel
{
    public string DocumentId { get; set; }
    public string LessonId { get; set; }
    public string Title { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
    public long? SizeInBytes { get; set; }

    public Lesson Lesson { get; set; }
}
