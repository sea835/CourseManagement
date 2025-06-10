using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CourseManagement.Core.Models;

public class Lesson : BaseModel
{
    [Key]
    public string LessonId { get; set; }
    [ForeignKey("ChapterId")]
    public string ChapterId { get; set; }
    public string Title { get; set; }
    public int? OrderNumber { get; set; }
    public string LessonType { get; set; }
    public int? Duration { get; set; }
    public bool IsPreviewable { get; set; }

    public Chapter Chapter { get; set; }
    public ICollection<Content> Contents  { get; set; }
    public ICollection<Video> Videos { get; set; }
    public ICollection<Document> Documents { get; set; }
}
