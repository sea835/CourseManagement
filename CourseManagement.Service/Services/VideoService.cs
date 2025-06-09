using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;
using CourseManagement.Service.IServices;
using Microsoft.AspNetCore.Http;

namespace CourseManagement.Service.Services
{
    public class VideoService(IUnitOfWork unitOfWork) : IVideoService
    {
        public async Task<IEnumerable<VideoViewModel>> GetAllVideos()
        {
            var videoViewModels = (
                from video in unitOfWork.Video.GetAll().ToList()
                join lesson in unitOfWork.Lesson.GetAll().ToList()
                    on video.LessonId equals lesson.LessonId
                join chapter in unitOfWork.Chapter.GetAll().ToList()
                    on lesson.ChapterId equals chapter.ChapterId
                join course in unitOfWork.Course.GetAll().ToList()
                    on chapter.CourseId equals course.CourseId
                select new VideoViewModel
                {
                    VideoId = video.VideoId,
                    LessonId = video.LessonId,
                    Title = video.Title,
                    Url = video.Url,
                    Duration = video.Duration,
                    Provider = video.Provider,
                    CreatedAt = video.CreatedAt,
                    LessonTitle = lesson.Title,
                    ChapterTitle = chapter.Title,
                    CourseTitle = course.Title
                }).ToList();
            return videoViewModels;
        }

        public async Task<VideoViewModel> GetById(string id)
        {
            var v = unitOfWork.Video.GetById(id);
            if (v == null) return null;
            return new VideoViewModel {
                VideoId = v.VideoId,
                LessonId = v.LessonId,
                Title = v.Title,
                Url = v.Url,
                Duration = v.Duration,
                Provider = v.Provider,
                CreatedAt = v.CreatedAt
            };
        }

        public async Task Create(VideoViewModel model)
        {
            var v = new Video
            {
                VideoId = model.VideoId,
                LessonId = model.LessonId,
                Title = model.Title,
                Url = model.Url,
                Duration = model.Duration,
                Provider = model.Provider
            };
            await unitOfWork.Video.Add(v);
        }

        public async Task Update(VideoViewModel model)
        {
            var v = unitOfWork.Video.GetById(model.VideoId);
            if (v == null) return;

            v.Title = model.Title;
            v.Url = model.Url;
            v.Duration = model.Duration;
            v.Provider = model.Provider;

            await unitOfWork.Video.Update(v);
        }

        public async Task Delete(string id)
        {
            await unitOfWork.Video.Delete(id);
        }

        public async Task Restore(string id)
        {
            await unitOfWork.Video.Restore(id);
        }

        public IEnumerable<VideoViewModel> GetByLessonId(string lessonId)
        {
            return unitOfWork.Video.BuildQuery(v => v.LessonId == lessonId)
                        .Select(v => new VideoViewModel {
                            VideoId = v.VideoId,
                            LessonId = v.LessonId,
                            Title = v.Title,
                            Url = v.Url,
                            Duration = v.Duration,
                            Provider = v.Provider,
                            CreatedAt = v.CreatedAt
                        });
        }
        
        public async Task UploadAndCreateVideo(VideoViewModel model, IFormFile videoFile, string rootPath, Action<string> setUrlCallback)
        {
            if (videoFile == null || videoFile.Length == 0)
                throw new Exception("No video file provided.");

            var fileName = Guid.NewGuid() + Path.GetExtension(videoFile.FileName);
            var savePath = Path.Combine(rootPath, "wwwroot/videos", fileName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await videoFile.CopyToAsync(stream);
            }

            // Trả lại đường dẫn URL để controller sử dụng
            var url = $"/videos/{fileName}";
            setUrlCallback(url);

            model.VideoId = Guid.NewGuid().ToString();
            model.CreatedAt = DateTime.Now;
            model.Url = url;

            var video = new Video
            {
                VideoId = model.VideoId,
                LessonId = model.LessonId,
                Title = model.Title,
                Url = model.Url,
                Duration = model.Duration,
                Provider = model.Provider,
                CreatedAt = model.CreatedAt
            };

            await unitOfWork.Video.Add(video);
        }
        
        public async Task<(Stream FileStream, string ContentType)> GetVideoStreamById(string id, string webRootPath)
        {
            var video = await GetById(id);
            if (video == null || string.IsNullOrEmpty(video.Url))
                return (null, null);

            var relativePath = video.Url.TrimStart('/');
            var absolutePath = Path.Combine(webRootPath, relativePath);

            if (!System.IO.File.Exists(absolutePath))
                return (null, null);

            var stream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read);
            return (stream, "video/mp4");
        }

        public IEnumerable<VideoViewModel> GetAllVideosByLessonId(string lessonId)
        {
            var videos = unitOfWork.Video.BuildQuery(v => v.LessonId == lessonId)
                .Select(v => new VideoViewModel
                {
                    VideoId = v.VideoId,
                    LessonId = v.LessonId,
                    Title = v.Title,
                    Url = v.Url,
                    Duration = v.Duration,
                    Provider = v.Provider,
                    CreatedAt = v.CreatedAt
                }).ToList();
            return videos;
        }
    }
}