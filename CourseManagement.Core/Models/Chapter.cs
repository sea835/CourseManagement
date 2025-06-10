using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Core.Models;

public class Chapter : BaseModel
{
    [Key]
    public string ChapterId { get; set; }
    [ForeignKey("CourseId")]
    public string CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FullDescription { get; set; }
    public int? OrderNumber { get; set; }

    public Course Course { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
}
