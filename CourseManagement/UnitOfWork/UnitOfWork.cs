using CourseManagement.Models;
using CourseManagement.IRepository;
using CourseManagement.Repository;

namespace CourseManagement.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ICourseRepository Course { get; private set; }
        public IDocumentRepository Document { get; private set; }
        public IVideoRepository Video { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IChapterRepository Chapter { get; private set; }
        public ILessonRepository Lesson { get; private set; }
        public IContentRepository Content { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Course = new CourseRepository(_context);
            Document = new DocumentRepository(_context);
            Video = new VideoRepository(_context);
            Category = new CategoryRepository(_context);
            Chapter = new ChapterRepository(_context);
            Lesson = new LessonRepository(_context);
            Content = new ContentRepository(_context);
        }
        
        public void SaveChange()
        {
            _context.SaveChanges();
        }
    }
}