using CourseManagement.Core.Interfaces.IRepository;
using CourseManagement.Core.Models;
using CourseManagement.Data.DbContext;

namespace CourseManagement.Data.Repository;

public class ChapterRepository: GenericRepository<Chapter>, IChapterRepository
{
    public ChapterRepository(AppDbContext context) : base(context) { }

    public IEnumerable<Chapter> GetAllChaptersByCourseId(string courseId)
    {
        var chapters = context.Chapters
            .Where(chapter => chapter.CourseId == courseId).AsEnumerable();
        return chapters;
    }
}