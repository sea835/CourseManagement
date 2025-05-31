using System.Linq.Expressions;
using PersonalExpenseTracker.ViewModels;

namespace CourseManagement.Repository
{
    public interface IRepository<T> where T : class
    {
        ResultViewModel Add(T entity);
        ResultViewModel Update(T entity);
        ResultViewModel GetById(string id);
        ResultViewModel GetAll();
        ResultViewModel Find(Expression<Func<T, bool>> predicate);
        ResultViewModel Delete(string id);      // Soft Delete nếu là BaseModel, Physical Delete nếu không
        ResultViewModel Restore(string id);     // Khôi phục nếu là soft delete (BaseModel)
        void SaveChanges();
    }
}