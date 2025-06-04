using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;

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
            var result = unitOfWork.Lesson.GetAll();
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
    
    public ResultViewModel GetLessonById(string id)
    {
        try
        {
            var result = unitOfWork.Lesson.GetById(id);
            return ResultViewModel.Success("Get lesson by id successfully", result);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel UpdateLesson(Lesson lesson)
    {
        try
        {
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
    
}