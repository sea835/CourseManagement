using System.Linq.Expressions;

namespace CourseManagement.Core.Interfaces.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task Update(T entity); 
        T GetById(string id);
        IQueryable<T> GetAll();
        IQueryable<T> BuildQuery(Expression<Func<T, bool>> predicate);
        Task Delete(string id);      
        Task Restore(string id);   
        void SaveChanges();
    }
}


