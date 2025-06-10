using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Core.Models;

public class Course: BaseModel
{
    [Key]
    public string CourseId { get; set; }
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
    [ForeignKey("CategoryId")]                    
    public string CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Chapter> Chapters { get; set; }
}
