namespace CourseManagement.Models;

public class Course: BaseModel
{
    public string CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FullDescription { get; set; }
    public string Level { get; set; }
    public int? Duration { get; set; }
    public string Language { get; set; }
    public string ThumbnailUrl { get; set; }
    public decimal? Price { get; set; }
    public bool IsFree { get; set; }
    public string AuthorName { get; set; }

    public string CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<Chapter> Chapters { get; set; }
}
