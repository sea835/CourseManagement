using System;
using CourseManagement.Core.RequestModels;
using CourseManagement.Core.ViewModels;
using CourseManagement.Core.Models;
using CourseManagement.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonController(ILessonService lessonService): ControllerBase
{
    [HttpGet]
    public ResultViewModel GetAllLessons()
    {
        try
        {
            var result = lessonService.GetAllLessons();
            if (result.Data is IEnumerable<LessonViewModel> list)
            {
                result.Data = list.Select(l => new LessonResponseModel
                {
                    LessonId = l.LessonId,
                    Title = l.Title,
                    OrderNumber = l.OrderNumber,
                    ChapterId = l.ChapterId,
                    Duration = l.Duration,
                    LessonType = l.LessonType,
                    IsPreviewable = l.IsPreviewable,
                    ChapterTitle = l.ChapterTitle,
                    CourseTitle = l.CourseTitle
                });
            }
            Response.StatusCode = result.Code;
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpGet("{id}")]
    public ResultViewModel GetLessonById(string id)
    {
        try
        {
            var l = lessonService.GetLessonById(id);
            if (l == null)
            {
                return ResultViewModel.Fail("Lesson not found");
            }
            var lesson = new LessonResponseModel
            {
                LessonId = l.LessonId,
                Title = l.Title,
                OrderNumber = l.OrderNumber,
                ChapterId = l.ChapterId,
                Duration = l.Duration,
                LessonType = l.LessonType,
                IsPreviewable = l.IsPreviewable,
                ChapterTitle = l.ChapterTitle,
                CourseTitle = l.CourseTitle
            };
            return ResultViewModel.Success("Get lesson by id successfully", lesson);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPost]
    public ResultViewModel CreateLesson([FromBody] LessonRequestModel model)
    {
        try
        {
            var lesson = new Lesson
            {
                LessonId = Guid.NewGuid().ToString(),
                Title = model.Title,
                OrderNumber = model.OrderNumber,
                ChapterId = model.ChapterId,
                Duration = model.Duration,
                LessonType = model.LessonType,
                IsPreviewable = model.IsPreviewable
            };
            var result = lessonService.CreateLesson(lesson);
            Response.StatusCode = result.Code;
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPut("{id}")]
    public ResultViewModel UpdateLesson(string id, [FromBody] LessonRequestModel model)
    {
        try
        {
            var lesson = new Lesson
            {
                LessonId = id,
                Title = model.Title,
                OrderNumber = model.OrderNumber,
                ChapterId = model.ChapterId,
                Duration = model.Duration,
                LessonType = model.LessonType,
                IsPreviewable = model.IsPreviewable
            };
            var result = lessonService.UpdateLesson(lesson);
            Response.StatusCode = result.Code;
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpDelete("{id}")]
    public ResultViewModel DeleteLesson(string id)
    {
        try
        {
            var result = lessonService.DeleteLesson(id);
            Response.StatusCode = result.Code;
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
}