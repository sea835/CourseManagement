using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Core.RequestModels;

public class LessonRequestModel
{
    [Required]
    public string ChapterId { get; set; }
    [Required]
    public string Title { get; set; }
    public int? OrderNumber { get; set; }
    public string LessonType { get; set; }
    public int? Duration { get; set; }
    public bool IsPreviewable { get; set; }
}
