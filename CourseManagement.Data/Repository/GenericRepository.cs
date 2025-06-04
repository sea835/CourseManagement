using System.Linq.Expressions;
using CourseManagement.Core.Interfaces.IRepository;
using CourseManagement.Core.Models;
using CourseManagement.Data.DbContext;

namespace CourseManagement.Data.Repository;

public abstract class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext context;

    public GenericRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task Add(T entity)
    {
        if (entity is BaseModel baseModel)
        {
            baseModel.SetCreated();
        }

        context.Set<T>().Add(entity);
        await SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        if (entity is BaseModel baseModel)
        {
            baseModel.SetUpdated();
        }

        context.Set<T>().Update(entity);
        await SaveChangesAsync();
    }

    public T GetById(string id)
    {
        return context.Set<T>().Find(id);
    }

    public virtual IEnumerable<T> GetAll()
    {
        var entities = context.Set<T>();
        return entities;
    }

    public IEnumerable<T> BuildQuery(Expression<Func<T, bool>> predicate)
    {
        var entities = context.Set<T>().Where(predicate);
        return entities;
    }

    public virtual async Task Delete(string id)
    {
        var entity = context.Set<T>().Find(id);
        if (entity == null)
            return;

        if (entity is BaseModel baseModel)
        {
            baseModel.SoftDelete();
            context.Set<T>().Update(entity); // soft delete
        }
        else
        {
            context.Set<T>().Remove(entity); // hard delete
        }

        await SaveChangesAsync();
    }

    public async Task Restore(string id)
    {
        var entity = context.Set<T>().Find(id);
        if (entity == null)
            return;

        if (entity is BaseModel baseModel)
        {
            baseModel.Restore();
            context.Set<T>().Update(entity);
            await SaveChangesAsync();
        }
        // else không hỗ trợ restore
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
