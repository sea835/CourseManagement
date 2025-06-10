using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Core.Models;

public class Document : BaseModel
{
    [Key]
    public string DocumentId { get; set; }
    [ForeignKey("LessonId")]
    public string LessonId { get; set; }
    public string Title { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
    public long? SizeInBytes { get; set; }

    public Lesson Lesson { get; set; }
}
