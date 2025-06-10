using CourseManagement.Core.Models;
using System.Linq;
using CourseManagement.Core.RequestModels;
using CourseManagement.Core.ViewModels;
using CourseManagement.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChapterController(IChapterService chapterService):ControllerBase
{
    [HttpGet]
    public IActionResult GetAllChapter()
    {
        var chapters = chapterService.GetAllChapters()
            .Select(c => new ChapterResponseModel
            {
                ChapterId = c.ChapterId,
                CourseId = c.CourseId,
                Title = c.Title,
                Description = c.Description,
                OrderNumber = c.OrderNumber,
                CourseTitle = c.CourseTitle
            });
        return Ok(chapters);
    }

    [HttpGet("{id}")]
    public IActionResult GetChapterById(string id)
    {
        var c = chapterService.GetChapterById(id);
        if (c == null) return NotFound();
        var chapter = new ChapterResponseModel
        {
            ChapterId = c.ChapterId,
            CourseId = c.CourseId,
            Title = c.Title,
            Description = c.Description,
            OrderNumber = c.OrderNumber
        };
        return Ok(chapter);
    }

    [HttpPost]
    public ResultViewModel CreateChapter([FromBody] ChapterRequestModel model)
    {
        var chapter = new Chapter
        {
            CourseId = model.CourseId,
            Title = model.Title,
            Description = model.Description,
            FullDescription = model.FullDescription,
            OrderNumber = model.OrderNumber
        };
        var result = chapterService.CreateChapter(chapter);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpPut("{id}")]
    public ResultViewModel UpdateChapter(string id, [FromBody] ChapterRequestModel model)
    {
        var chapter = new Chapter
        {
            ChapterId = id,
            CourseId = model.CourseId,
            Title = model.Title,
            Description = model.Description,
            FullDescription = model.FullDescription,
            OrderNumber = model.OrderNumber
        };
        var result = chapterService.UpdateChapter(chapter);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpDelete("{id}")]
    public ResultViewModel DeleteChapter(string id)
    {
        var result = chapterService.DeleteChapter(id);
        Response.StatusCode = result.Code;
        return result;
    }
}