using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Core.RequestModels;

public class VideoRequestModel
{
    [Required]
    public string LessonId { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public int? Duration { get; set; }
    public string Provider { get; set; }
}
