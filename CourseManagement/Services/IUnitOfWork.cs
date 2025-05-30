using CourseManagement.Models;
using CourseManagement.Repository;

namespace CourseManagement.Services;

public interface IUnitOfWork
{
    IRepository<Course> CourseRepository { get; }
    IRepository<Category> CategoryRepository { get; }
    IRepository<Chapter> ChapterRepository { get; }
    IRepository<Lesson> LessionRepository { get; }
    IRepository<Content> ContentRepository { get; }
    IRepository<Video> VideoRepository { get; }
    IRepository<Document> DocumentRepository { get; }
    
    void SaveChange();
}