using CourseManagement.Core.Models;

namespace CourseManagement.Core.Interfaces.IRepository;

public interface IChapterRepository: IRepository<Chapter>
{
    public IEnumerable<Chapter> GetAllChaptersByCourseId(string courseId);
}