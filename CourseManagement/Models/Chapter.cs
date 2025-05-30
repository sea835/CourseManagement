namespace CourseManagement.Models;

public class Chapter : BaseModel
{
    public string ChapterId { get; set; }
    public string CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int? OrderNumber { get; set; }

    public Course Course { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
}
