using CourseManagement.Core.Models;
using System;
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
    public ResultViewModel GetAllChapter()
    {
        try
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
            return ResultViewModel.Success("Get all chapters successfully", chapters);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpGet("{id}")]
    public ResultViewModel GetChapterById(string id)
    {
        try
        {
            var c = chapterService.GetChapterById(id);
            if (c == null)
            {
                return ResultViewModel.Fail("Chapter not found");
            }
            var chapter = new ChapterResponseModel
            {
                ChapterId = c.ChapterId,
                CourseId = c.CourseId,
                Title = c.Title,
                Description = c.Description,
                OrderNumber = c.OrderNumber
            };
            return ResultViewModel.Success("Get chapter by id successfully", chapter);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPost]
    public ResultViewModel CreateChapter([FromBody] ChapterRequestModel model)
    {
        try
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
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPut("{id}")]
    public ResultViewModel UpdateChapter(string id, [FromBody] ChapterRequestModel model)
    {
        try
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
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpDelete("{id}")]
    public ResultViewModel DeleteChapter(string id)
    {
        try
        {
            var result = chapterService.DeleteChapter(id);
            Response.StatusCode = result.Code;
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}