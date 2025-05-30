using CourseManagement.Models;

namespace CourseManagement.Repository;

public class VideoRepository: GenericRepository<Video>
{
    public VideoRepository(AppDbContext context) : base(context) { }
}