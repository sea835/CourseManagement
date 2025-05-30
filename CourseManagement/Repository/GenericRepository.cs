using System.Linq.Expressions;
using CourseManagement.Models;

namespace CourseManagement.Repository;

public abstract class GenericRepository <T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    
    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public T Add(T entity)
    {
        _context.Set<T>().Add(entity);
        SaveChanges();
        return entity;
    }
    
    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        SaveChanges();
        return entity;
    }
    
    public T GetById(string id)
    {
        return _context.Set<T>().Find(id);
    }
    
    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }
    
    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate).ToList();
    }
    
    public void Delete(string id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            SaveChanges();
        }
    }
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}