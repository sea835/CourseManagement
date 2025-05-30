using CourseManagement.Models;
using CourseManagement.Repository;

namespace CourseManagement.Services;

public interface IUnitOfWork
{
    IRepository<Course> CourseRepository { get; }
    IRepository<Category> CategoryRepository { get; }
    
    void SaveChange();
}