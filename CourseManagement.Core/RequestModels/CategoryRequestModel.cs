using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Core.RequestModels;

public class CategoryRequestModel
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}
