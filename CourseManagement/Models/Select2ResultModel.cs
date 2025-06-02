namespace CourseManagement.Models;

public class Select2ResultModel
{
    public List<Select2ViewModel> Results { get; set; } = new List<Select2ViewModel>();
    public bool More { get; set; }
}