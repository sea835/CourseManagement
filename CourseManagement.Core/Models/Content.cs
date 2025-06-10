using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Core.Models;

public class Content : BaseModel
{
    [Key]
    public string ContentId { get; set; }
    [ForeignKey("LessonId")]
    public string LessonId { get; set; }
    public string MainContent { get; set; }
    public string Summary { get; set; }

    public Lesson Lesson { get; set; }
}
