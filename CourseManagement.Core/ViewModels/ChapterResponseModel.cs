namespace CourseManagement.Core.ViewModels;

public class ChapterResponseModel
{
    public string ChapterId { get; set; }
    public string CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FullDescription { get; set; }
    public int? OrderNumber { get; set; }
    public string CourseTitle { get; set; }
}
