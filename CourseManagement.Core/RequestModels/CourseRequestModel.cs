using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Core.RequestModels;

public class CourseRequestModel
{
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public string FullDescription { get; set; }
    public string Level { get; set; }
    public int? Duration { get; set; }
    public string Language { get; set; }
    public string ThumbnailUrl { get; set; }
    public double Price { get; set; }
    public bool IsFree { get; set; }
    public string AuthorName { get; set; }
    [Required]
    public string CategoryId { get; set; }
}
