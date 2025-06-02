namespace CourseManagement.Models;

public class CourseViewModel
{
    public string CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FullDescription { get; set; }
    public string Level { get; set; }
    public int? Duration { get; set; }
    public double Price { get; set; }
    public string Language { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Category { get; set; }
    public string AuthorName { get; set; }
}