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
    public IActionResult GetAllLessons()
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
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetLessonById(string id)
    {
        var l = lessonService.GetLessonById(id);
        if (l == null) return NotFound();
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
        return Ok(lesson);
    }

    [HttpPost]
    public ResultViewModel CreateLesson([FromBody] LessonRequestModel model)
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

    [HttpPut("{id}")]
    public ResultViewModel UpdateLesson(string id, [FromBody] LessonRequestModel model)
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

    [HttpDelete("{id}")]
    public ResultViewModel DeleteLesson(string id)
    {
        var result = lessonService.DeleteLesson(id);
        Response.StatusCode = result.Code;
        return result;
    }
    
}