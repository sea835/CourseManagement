using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonContentController(ILessonContentService lessonContentService) : ControllerBase
{
    [HttpGet("{lessonId}")]
    public IActionResult GetLessonContentByLessonId(string lessonId)
    {
        var content = lessonContentService.GetContentByLessonId(lessonId);
        return Ok(content);
    }

    [HttpGet("{id}")]
    public IActionResult GetContentByLessonId(string id)
    {
        var content = lessonContentService.GetContentByLessonId(id);
        return Ok(content);
    }

    [HttpPost]
    public ResultViewModel AddContent([FromBody] ContentViewModel content)
    {
        var result = lessonContentService.CreateContent(content);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpPut]
    public ResultViewModel UpdateContent([FromBody] ContentViewModel content)
    {
        var result = lessonContentService.UpdateContent(content);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpDelete("{id}")]
    public ResultViewModel DeleteContent(string id)
    {
        var result = lessonContentService.DeleteContent(id);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpPut("UpdateSummaryContent")]
    public ResultViewModel UpdateSummaryContent([FromBody] ContentViewModel content)
    {
        var result = lessonContentService.UpdateSummary(content.ContentId, content.Summary);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpPut("UpdateMainContent")]
    public ResultViewModel UpdateMainContent([FromBody] ContentViewModel content)
    {
        var result = lessonContentService.UpdateMainContent(content.ContentId, content.MainContent);
        Response.StatusCode = result.Code;
        return result;
    }
}