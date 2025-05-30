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
    
    public void SaveChange()
    {
        _context.SaveChanges();
    }

}