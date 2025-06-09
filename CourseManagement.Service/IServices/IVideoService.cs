using CourseManagement.Core.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CourseManagement.Service.IServices
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
        Task UploadAndCreateVideo(VideoViewModel model, IFormFile videoFile, string rootPath, Action<string> setUrlCallback);
        Task<(Stream FileStream, string ContentType)> GetVideoStreamById(string id, string webRootPath);
        public IEnumerable<VideoViewModel> GetAllVideosByLessonId(string lessonId);
    }
}