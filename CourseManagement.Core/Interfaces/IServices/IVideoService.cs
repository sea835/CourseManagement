using CourseManagement.Core.ViewModels;

namespace CourseManagement.Core.Interfaces.IServices
{
    public interface IVideoService
    {
        Task<IEnumerable<VideoViewModel>> GetAllVideos();
        Task<VideoViewModel> GetById(string id);
        Task Create(VideoViewModel video);
        Task Update(VideoViewModel video);
        Task Delete(string id);
        Task Restore(string id);
        IEnumerable<VideoViewModel> GetByLessonId(string lessonId);
    }
}