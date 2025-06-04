using CourseManagement.Core.Interfaces.IRepository;
using CourseManagement.Core.Models;
using CourseManagement.Data.DbContext;

namespace CourseManagement.Data.Repository;

public class ContentRepository: GenericRepository<Content>, IContentRepository
{
    public ContentRepository(AppDbContext context) : base(context) { }
}