using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;

namespace CourseManagement.Service.Services
{
    public class VideoService(IUnitOfWork unitOfWork) : IVideoService
    {
        public async Task<IEnumerable<VideoViewModel>> GetAllVideos()
        {
            var courses = unitOfWork.Course.GetAll().ToList();
            var chapters = unitOfWork.Chapter.GetAll().ToList();
            var lessons = unitOfWork.Lesson.GetAll().ToList();
            var videos = unitOfWork.Video.GetAll().ToList();
            var videoViewModels = (from video in videos
                join lesson in lessons
                    on video.LessonId equals lesson.LessonId into lessonGroup
                from l in lessonGroup.DefaultIfEmpty()
                join chapter in chapters
                    on l?.ChapterId equals chapter.ChapterId into chapterGroup
                from c in chapterGroup.DefaultIfEmpty()
                join course in courses
                    on c?.CourseId equals course.CourseId into courseGroup
                from cr in courseGroup.DefaultIfEmpty()
                select new VideoViewModel
                {
                    VideoId = video.VideoId,
                    LessonId = video.LessonId,
                    Title = video.Title,
                    Url = video.Url,
                    Duration = video.Duration,
                    Provider = video.Provider,
                    CreatedAt = video.CreatedAt,
                    CourseTitle = cr?.Title,
                    LessonTitle = l?.Title,
                    ChapterTitle = c?.Title
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
    }
}