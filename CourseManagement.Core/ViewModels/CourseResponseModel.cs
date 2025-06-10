namespace CourseManagement.Core.ViewModels;

public class CourseResponseModel
{
    public string CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Level { get; set; }
    public int? Duration { get; set; }
    public string Language { get; set; }
    public string ThumbnailUrl { get; set; }
    public double Price { get; set; }
    public bool IsFree { get; set; }
    public string AuthorName { get; set; }
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
}
