using CourseManagement.IRepository;
using CourseManagement.Models;

namespace CourseManagement.Repository;

public class VideoRepository: GenericRepository<Video>, IVideoRepository
{
    public VideoRepository(AppDbContext context) : base(context) { }
}