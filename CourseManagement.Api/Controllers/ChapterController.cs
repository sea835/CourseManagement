using CourseManagement.Core.Models;
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
        var chapters = chapterService.GetAllChapters();
        return Ok(chapters);
    }

    [HttpGet("{id}")]
    public IActionResult GetChapterById(string id)
    {
        var chapter = chapterService.GetChapterById(id);
        return Ok(chapter);
    }

    [HttpPost]
    public ResultViewModel CreateChapter([FromBody] ChapterViewModel chapter)
    {
        var result = chapterService.CreateChapter(chapter);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpPut]
    public ResultViewModel UpdateChapter([FromBody] ChapterViewModel chapter)
    {
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