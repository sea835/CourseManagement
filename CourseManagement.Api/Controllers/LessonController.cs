using CourseManagement.Core.ViewModels;
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
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetLessonById(string id)
    {
        var lesson = lessonService.GetLessonById(id);
        return Ok(lesson);
    }

    [HttpPost]
    public ResultViewModel CreateLesson([FromBody] LessonViewModel lesson)
    {
        var result = lessonService.CreateLesson(lesson);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpPut]
    public ResultViewModel UpdateLesson([FromBody] LessonViewModel lesson)
    {
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