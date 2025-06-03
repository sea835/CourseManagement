using CourseManagement.Models;
using CourseManagement.ViewModels;

namespace CourseManagement.IServices;

public interface IChapterService
{
    public ResultViewModel GetAllChapters();
    // public ResultViewModel GetAllChaptersByCourseId(string courseId);
    public ResultViewModel GetChapterById(string id);
    public ResultViewModel UpdateChapter(Chapter chapter);
    public ResultViewModel CreateChapter(Chapter chapter);
    public ResultViewModel DeleteChapter(string id);
}