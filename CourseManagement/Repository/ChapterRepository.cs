using CourseManagement.IRepository;
using CourseManagement.Models;

namespace CourseManagement.Repository;

public class ChapterRepository: GenericRepository<Chapter>, IChapterRepository
{
    public ChapterRepository(AppDbContext context) : base(context) { }

    public IEnumerable<Chapter> GetChaptersByCourseId(string courseId)
    {
        return context.Chapters
            .Where(chapter => chapter.CourseId == courseId);
    }
}