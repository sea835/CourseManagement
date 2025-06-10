using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Core.RequestModels;

public class ContentRequestModel
{
    [Required]
    public string LessonId { get; set; }
    public string MainContent { get; set; }
    public string Summary { get; set; }
}
