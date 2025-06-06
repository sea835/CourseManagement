using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;

namespace CourseManagement.Core.Interfaces.IServices;

public interface IChapterService
{
    public IEnumerable<ChapterViewModel> GetAllChapters();
    // public ResultViewModel GetAllChaptersByCourseId(string courseId);
    public ChapterViewModel GetChapterById(string id);
    public ResultViewModel UpdateChapter(Chapter chapter);
    public ResultViewModel CreateChapter(Chapter chapter);
    public ResultViewModel DeleteChapter(string id);
    public ResultViewModel GetAllChaptersSelect2();
    public ResultViewModel GetAllChaptersByCourseIdSelect2(string courseId);
}