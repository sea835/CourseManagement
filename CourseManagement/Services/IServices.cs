using System.Linq.Expressions;

namespace CourseManagement.Services;

public interface IServices<T>
{
    public T Add(T entity);


    public T Update(T entity);


    public T GetById(string id);

    public IEnumerable<T> GetAll();

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

    public void Delete(string id);

    public void SaveChanges();

}