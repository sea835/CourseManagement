using System.Linq.Expressions;
using CourseManagement.Models;
using PersonalExpenseTracker.ViewModels;

namespace CourseManagement.Repository;

public abstract class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext context;

    public GenericRepository(AppDbContext context)
    {
        this.context = context;
    }

    public ResultViewModel Add(T entity)
    {
        try
        {
            if (entity is BaseModel baseModel)
            {
                baseModel.SetCreated();
            }

            context.Set<T>().Add(entity);
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

            context.Set<T>().Update(entity);
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
            var entity = context.Set<T>().Find(id);
            if (entity == null)
                return ResultViewModel.Fail($"No {typeof(T).Name} found with id = {id}");
            return ResultViewModel.Success(null, entity);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public virtual ResultViewModel GetAll()
    {
        try
        {
            var data = context.Set<T>().ToList();
            return ResultViewModel.Success(null, data);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public virtual ResultViewModel Find(Expression<Func<T, bool>> predicate)
    {
        try
        {
            var data = context.Set<T>().Where(predicate).ToList();
            return ResultViewModel.Success(null, data);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    // Soft Delete (if BaseModel), else Physical Delete
    public virtual ResultViewModel Delete(string id)
    {
        try
        {
            var entity = context.Set<T>().Find(id);
            if (entity == null)
                return ResultViewModel.Fail($"No {typeof(T).Name} found with id = {id}");

            if (entity is BaseModel baseModel)
            {
                baseModel.SoftDelete();
                context.Set<T>().Update(entity); // update trạng thái soft delete
            }
            else
            {
                context.Set<T>().Remove(entity); // nếu không phải BaseModel thì xóa cứng
            }

            SaveChanges();
            return ResultViewModel.Success("Deleted successfully", entity);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public virtual void SaveChanges()
    {
        context.SaveChanges();
    }

    // Optional: Restore (cho soft-delete)
    public virtual ResultViewModel Restore(string id)
    {
        try
        {
            var entity = context.Set<T>().Find(id);
            if (entity == null)
                return ResultViewModel.Fail($"No {typeof(T).Name} found with id = {id}");

            if (entity is BaseModel baseModel)
            {
                baseModel.Restore();
                context.Set<T>().Update(entity);
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
