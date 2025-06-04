using CourseManagement.Core.Interfaces.IRepository;
using CourseManagement.Core.Models;
using CourseManagement.Data.DbContext;

namespace CourseManagement.Data.Repository;

public class VideoRepository: GenericRepository<Video>, IVideoRepository
{
    public VideoRepository(AppDbContext context) : base(context) { }
}