using System.Linq.Expressions;

namespace CourseManagement.Repository;

public interface IRepository<T>
{
    T Add(T entity);
    T Update(T entity);
    T GetById(string id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    void Delete(string id);
    void SaveChanges();
}