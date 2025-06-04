namespace CourseManagement.Core.Models;

public class Category : BaseModel
{
    public string CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }

    public ICollection<Course> Courses { get; set; }
}