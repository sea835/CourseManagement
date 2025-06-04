using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;

namespace CourseManagement.Service.Services;

public class ChapterService: IChapterService
{
    private readonly IUnitOfWork unitOfWork;

    public ChapterService(IUnitOfWork unitOfWork)
    {
       this.unitOfWork = unitOfWork;
    }

    public ResultViewModel GetAllChapters()
    {
        try
        {
            var chapters = unitOfWork.Chapter.GetAll();
            return ResultViewModel.Success("Get all chapters successfully", chapters);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
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
    
    public ResultViewModel GetChapterById(string id)
    {
        try
        {
            var chapter = unitOfWork.Chapter.Find(c => c.ChapterId == id);
            return ResultViewModel.Success("Get chapter by id: " + id + " successfully", chapter);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel UpdateChapter(Chapter chapter)
    {
        try
        {
            unitOfWork.Chapter.Update(chapter);
            unitOfWork.SaveChange();
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
            unitOfWork.SaveChange();
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
            unitOfWork.SaveChange();
            return ResultViewModel.Success("Delete chapter successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}