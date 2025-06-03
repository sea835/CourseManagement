using CourseManagement.IRepository;
using CourseManagement.Models;
using CourseManagement.Repository;

namespace CourseManagement.UnitOfWork;

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

