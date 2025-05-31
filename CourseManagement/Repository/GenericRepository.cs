using System.Linq.Expressions;
using CourseManagement.Models;
using PersonalExpenseTracker.ViewModels;

namespace CourseManagement.Repository;

public abstract class GenericRepository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public ResultViewModel Add(T entity)
    {
        try
        {
            if (entity is BaseModel baseModel)
            {
                baseModel.SetCreated();
            }

            _context.Set<T>().Add(entity);
            SaveChanges();
            return ResultViewModel.Success("Added successfully", entity);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public ResultViewModel Update(T entity)
    {
        try
        {
            if (entity is BaseModel baseModel)
            {
                baseModel.SetUpdated();
            }

            _context.Set<T>().Update(entity);
            SaveChanges();
            return ResultViewModel.Success("Updated successfully", entity);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public ResultViewModel GetById(string id)
    {
        try
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
                return ResultViewModel.Fail($"No {typeof(T).Name} found with id = {id}");
            return ResultViewModel.Success(null, entity);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public ResultViewModel GetAll()
    {
        try
        {
            var data = _context.Set<T>().ToList();
            return ResultViewModel.Success(null, data);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public ResultViewModel Find(Expression<Func<T, bool>> predicate)
    {
        try
        {
            var data = _context.Set<T>().Where(predicate).ToList();
            return ResultViewModel.Success(null, data);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    // Soft Delete (if BaseModel), else Physical Delete
    public ResultViewModel Delete(string id)
    {
        try
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
                return ResultViewModel.Fail($"No {typeof(T).Name} found with id = {id}");

            if (entity is BaseModel baseModel)
            {
                baseModel.SoftDelete();
                _context.Set<T>().Update(entity); // update trạng thái soft delete
            }
            else
            {
                _context.Set<T>().Remove(entity); // nếu không phải BaseModel thì xóa cứng
            }

            SaveChanges();
            return ResultViewModel.Success("Deleted successfully", entity);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    // Optional: Restore (cho soft-delete)
    public ResultViewModel Restore(string id)
    {
        try
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
                return ResultViewModel.Fail($"No {typeof(T).Name} found with id = {id}");

            if (entity is BaseModel baseModel)
            {
                baseModel.Restore();
                _context.Set<T>().Update(entity);
                SaveChanges();
                return ResultViewModel.Success("Restored successfully", entity);
            }
            else
            {
                return ResultViewModel.Fail($"Restore not supported for {typeof(T).Name}.");
            }
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}
