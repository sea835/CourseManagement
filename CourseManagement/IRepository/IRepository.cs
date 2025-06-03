using System.Linq.Expressions;
using CourseManagement.ViewModels;

namespace CourseManagement.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task Update(T entity); 
        T GetById(string id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task Delete(string id);      
        Task Restore(string id);   
        void SaveChanges();
    }
}


