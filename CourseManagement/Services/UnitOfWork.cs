using CourseManagement.Models;
using CourseManagement.Repository;

namespace CourseManagement.Services;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    private IRepository<Course> courseRepository;
    public IRepository<Course> CourseRepository
    {
        get { return courseRepository ??= new CourseRepository(_context); }
    }
    
    private IRepository<Category> categoryRepository;
    public IRepository<Category> CategoryRepository
    {
        get { return categoryRepository ??= new CategoryRepository(_context); }
    }
    
    private IRepository<Chapter> chapterRepository;
    public IRepository<Chapter> ChapterRepository
    {
        get { return chapterRepository ??= new ChapterRepository(_context); }
    }
    
    private IRepository<Lesson> lessonRepository;
    public IRepository<Lesson> LessionRepository
    {
        get { return lessonRepository ??= new LessonRepository(_context); }
    }
    
    private IRepository<Content> contentRepository;
    public IRepository<Content> ContentRepository
    {
        get { return contentRepository ??= new ContentRepository(_context); }
    }
    
    private IRepository<Video> videoRepository;
    public IRepository<Video> VideoRepository
    {
        get { return videoRepository ??= new VideoRepository(_context); }
    }
    
    private IRepository<Document> documentRepository;
    public IRepository<Document> DocumentRepository
    {
        get { return documentRepository ??= new DocumentRepository(_context); }
    }
    
    public void SaveChange()
    {
        _context.SaveChanges();
    }

}