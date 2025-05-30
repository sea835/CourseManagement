namespace CourseManagement.Services;
using CourseManagement.Models;
using CourseManagement.Repository;

public class CourseServices : GenericServices<Course>
{
    private readonly IRepository<Course> CourseRepository;
    
    public CourseServices(IRepository<Course> repository) : base(repository)
    {
        CourseRepository = repository;
    }
}