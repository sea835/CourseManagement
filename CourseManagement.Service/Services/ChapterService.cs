using System.Collections;
using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;
using CourseManagement.Service.IServices;

namespace CourseManagement.Service.Services;

public class ChapterService : IChapterService
{
    private readonly IUnitOfWork unitOfWork;

    public ChapterService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public IEnumerable<ChapterViewModel> GetAllChapters()
    {
        try
        {
            var chapters = unitOfWork.Chapter.GetAll().Where(c => c.IsActive).ToList();
            var courses = unitOfWork.Course.GetAll().ToList();
            var chapterViewModel =
                    from chapter in chapters
                    join course in courses on chapter.CourseId equals course.CourseId
                    select new ChapterViewModel
                    {
                        ChapterId = chapter.ChapterId,
                        Title = chapter.Title,
                        Description = chapter.Description,
                        CourseId = chapter.CourseId,
                        OrderNumber = chapter.OrderNumber,
                        CourseTitle = course.Title
                    }
                ;
            return chapterViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // public ResultViewModel GetAllChaptersByCourseId(string courseId)
    // {
    //     try
    //     {
    //         var chapters = unitOfWork.Chapter.GetChaptersByCourseId(courseId);
    //         return ResultViewModel.Success("Get all chapters by course id successfully", chapters);
    //     }
    //     catch (Exception ex)
    //     {
    //         return ResultViewModel.FailException(ex);
    //     }
    // }

    public ChapterViewModel GetChapterById(string id)
    {
        try
        {
            var chapter = unitOfWork.Chapter.BuildQuery(c => c.ChapterId == id).FirstOrDefault();
            return new ChapterViewModel
            {
                ChapterId = chapter.ChapterId,
                Title = chapter.Title,
                Description = chapter.Description,
                CourseId = chapter.CourseId,
                OrderNumber = chapter.OrderNumber,
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving chapter by ID", ex);
        }
    }

    public ResultViewModel UpdateChapter(Chapter chapter)
    {
        try
        {
            var existingChapter = unitOfWork.Chapter.BuildQuery(c => c.ChapterId == chapter.ChapterId).FirstOrDefault();
            existingChapter.CourseId = chapter.CourseId;
            existingChapter.Title = chapter.Title;
            existingChapter.Description = chapter.Description;
            existingChapter.OrderNumber = chapter.OrderNumber;
            existingChapter.SetUpdated();
            unitOfWork.Chapter.Update(chapter);
            // unitOfWork.SaveChange();
            return ResultViewModel.Success("Update chapter successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public ResultViewModel CreateChapter(Chapter chapter)
    {
        try
        {
            chapter.ChapterId = Guid.NewGuid().ToString();
            unitOfWork.Chapter.Add(chapter);
            // unitOfWork.SaveChange();
            return ResultViewModel.Success("Create new chapter successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public ResultViewModel DeleteChapter(string id)
    {
        try
        {
            unitOfWork.Chapter.Delete(id);
            // unitOfWork.SaveChange();
            return ResultViewModel.Success("Delete chapter successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel GetAllChaptersSelect2()
    {
        try
        {
            var chapters = unitOfWork.Chapter.GetAll().Where(c => c.IsActive).ToList();
            var select2Data = chapters.Select(c => new
            {
                id = c.ChapterId,
                text = c.Title
            }).ToList();
            return ResultViewModel.Success("Get all chapters for Select2 successfully", select2Data);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel GetAllChaptersByCourseIdSelect2(string courseId)
    {
        try
        {
            var chapters = unitOfWork.Chapter.GetAllChaptersByCourseId(courseId).Where(c => c.IsActive).ToList();
            var select2Data = chapters.Select(c => new
            {
                id = c.ChapterId,
                text = c.Title
            }).ToList();
            return ResultViewModel.Success("Get all chapters by course id for Select2 successfully", select2Data);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}