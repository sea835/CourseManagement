namespace CourseManagement.Services;
using CourseManagement.Models;
using CourseManagement.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

public abstract class GenericServices<T> : IServices<T> where T : class
{
    private readonly IRepository<T> _repository;

    public GenericServices(IRepository<T> repository)
    {
        _repository = repository;
    }

    public T Add(T entity)
    {
        return _repository.Add(entity);
    }

    public T Update(T entity)
    {
        return _repository.Update(entity);
    }

    public T GetById(string id)
    {
        return _repository.GetById(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _repository.GetAll();
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _repository.Find(predicate);
    }

    public void Delete(string id)
    {
        _repository.Delete(id);
    }

    public void SaveChanges()
    {
        _repository.SaveChanges();
    }
}