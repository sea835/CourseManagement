using CourseManagement.Core.Interfaces.IRepository;

namespace CourseManagement.Data.UnitOfWork;

public interface IUnitOfWork
{
    ICourseRepository Course { get; }
    ICategoryRepository Category { get; }
    IChapterRepository Chapter { get; }
    ILessonRepository Lesson { get; }
    IContentRepository Content { get; }
    IVideoRepository Video { get; }
    IDocumentRepository Document { get; }
    
    void SaveChange();
}

