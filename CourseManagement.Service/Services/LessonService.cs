using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;
using CourseManagement.Service.IServices;

namespace CourseManagement.Service.Services;

public class LessonService: ILessonService
{
    private readonly IUnitOfWork unitOfWork;

    public LessonService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    
    public ResultViewModel GetAllLessons()
    {
        try
        {
            var lessons = unitOfWork.Lesson.GetAll().ToList();
            var courses = unitOfWork.Course.GetAll().ToList();
            var chapters = unitOfWork.Chapter.GetAll().ToList();
            var result = 
                from lesson in lessons
                join chapter in chapters on lesson.ChapterId equals chapter.ChapterId
                join course in courses on chapter.CourseId equals course.CourseId
                select new LessonViewModel
                {
                    LessonId = lesson.LessonId,
                    Title = lesson.Title,
                    OrderNumber = lesson.OrderNumber,
                    ChapterId = lesson.ChapterId,
                    Duration = lesson.Duration,
                    LessonType = lesson.LessonType,
                    IsPreviewable = lesson.IsPreviewable,
                    ChapterTitle = chapter.Title,
                    CourseTitle = course.Title
                };
            return ResultViewModel.Success("Get all lessons successfully", result);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel DeleteLesson(string id)
    {
        try
        {
            unitOfWork.Lesson.Delete(id);
            return ResultViewModel.Success("Delete lesson successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public LessonViewModel GetLessonById(string id)
    {
        try
        {
            var lesson = unitOfWork.Lesson.BuildQuery(l => l.LessonId == id).FirstOrDefault();
            var chapter = unitOfWork.Chapter.BuildQuery(c => c.ChapterId == lesson.ChapterId).FirstOrDefault();
            var course = unitOfWork.Course.BuildQuery(c => c.CourseId == chapter.CourseId).FirstOrDefault();
            var lessonViewModel = new LessonViewModel
            {
                LessonId = lesson.LessonId,
                Title = lesson.Title,
                OrderNumber = lesson.OrderNumber,
                ChapterId = lesson.ChapterId,
                Duration = lesson.Duration,
                LessonType = lesson.LessonType,
                IsPreviewable = lesson.IsPreviewable,
                ChapterTitle = chapter.Title,
                CourseTitle = course.Title
            };
            return lessonViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public ResultViewModel UpdateLesson(Lesson lesson)
    {
        try
        {
            var existingLesson = unitOfWork.Lesson.BuildQuery(l => l.LessonId == lesson.LessonId).FirstOrDefault();
            existingLesson.LessonId = lesson.LessonId;
            existingLesson.Title = lesson.Title;
            existingLesson.OrderNumber = lesson.OrderNumber;
            existingLesson.ChapterId = lesson.ChapterId;
            existingLesson.Duration = lesson.Duration;
            existingLesson.LessonType = lesson.LessonType;
            existingLesson.IsPreviewable = lesson.IsPreviewable;
            unitOfWork.Lesson.Update(lesson);
            return ResultViewModel.Success("Update lesson by id successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel CreateLesson(Lesson lesson)
    {
        try
        {
            unitOfWork.Lesson.Add(lesson);
            return ResultViewModel.Success("Create lesson by id successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public IEnumerable<LessonViewModel> GetAllLessonsByChapterId(string chapterId)
    {
        var lessons = unitOfWork.Lesson.GetAll().ToList().Select(l => new LessonViewModel
        {
            LessonId = l.LessonId,
            Title = l.Title,
            OrderNumber = l.OrderNumber,
            ChapterId = l.ChapterId,
            Duration = l.Duration,
            LessonType = l.LessonType,
            IsPreviewable = l.IsPreviewable,
        }).Where(l => l.ChapterId == chapterId).ToList();
        return lessons;
    }

    public IEnumerable<Select2ViewModel> GetAllLessonsByChapterIdSelect2(string chapterId)
    {
        var lessons = unitOfWork.Lesson.GetAll().ToList()
            .Where(l => l.ChapterId == chapterId)
            .Select(l => new Select2ViewModel()
        {
            Id = l.LessonId,
            Text = l.Title,
        }).ToList();
        return lessons;
    }
}