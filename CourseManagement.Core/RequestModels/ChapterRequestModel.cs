using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Core.RequestModels;

public class ChapterRequestModel
{
    [Required]
    public string CourseId { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public string FullDescription { get; set; }
    public int? OrderNumber { get; set; }
}
