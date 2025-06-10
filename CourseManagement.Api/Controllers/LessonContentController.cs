using System;
﻿using CourseManagement.Core.Models;
using CourseManagement.Core.RequestModels;
using CourseManagement.Core.ViewModels;
using CourseManagement.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonContentController(ILessonContentService lessonContentService) : ControllerBase
{
    [HttpGet("lesson/{lessonId}")]
    public ResultViewModel GetLessonContentByLessonId(string lessonId)
    {
        try
        {
            var content = lessonContentService.GetContentByLessonId(lessonId);
            if (content == null)
            {
                return ResultViewModel.Fail("Content not found");
            }
            var result = new ContentResponseModel
            {
                ContentId = content.ContentId,
                LessonId = content.LessonId,
                MainContent = content.MainContent,
                Summary = content.Summary
            };
            return ResultViewModel.Success("Get content by lesson id successfully", result);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpGet("{id}")]
    public ResultViewModel GetContentById(string id)
    {
        try
        {
            var c = lessonContentService.GetById(id);
            if (c == null)
            {
                return ResultViewModel.Fail("Content not found");
            }
            var content = new ContentResponseModel
            {
                ContentId = c.ContentId,
                LessonId = c.LessonId,
                MainContent = c.MainContent,
                Summary = c.Summary
            };
            return ResultViewModel.Success("Get content by id successfully", content);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPost]
    public ResultViewModel AddContent([FromBody] ContentRequestModel model)
    {
        try
        {
            var content = new ContentViewModel
            {
                ContentId = Guid.NewGuid().ToString(),
                LessonId = model.LessonId,
                MainContent = model.MainContent,
                Summary = model.Summary
            };
            var result = lessonContentService.CreateContent(content);
            Response.StatusCode = result.Code;
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPut("{id}")]
    public ResultViewModel UpdateContent(string id, [FromBody] ContentRequestModel model)
    {
        try
        {
            var content = new ContentViewModel
            {
                ContentId = id,
                LessonId = model.LessonId,
                MainContent = model.MainContent,
                Summary = model.Summary
            };
            var result = lessonContentService.UpdateContent(content);
            Response.StatusCode = result.Code;
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpDelete("{id}")]
    public ResultViewModel DeleteContent(string id)
    {
        try
        {
            var result = lessonContentService.DeleteContent(id);
            Response.StatusCode = result.Code;
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPut("UpdateSummaryContent/{id}")]
    public ResultViewModel UpdateSummaryContent(string id, [FromBody] ContentRequestModel content)
    {
        try
        {
            var result = lessonContentService.UpdateSummary(id, content.Summary);
            Response.StatusCode = result.Code;
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPut("UpdateMainContent/{id}")]
    public ResultViewModel UpdateMainContent(string id, [FromBody] ContentRequestModel content)
    {
        try
        {
            var result = lessonContentService.UpdateMainContent(id, content.MainContent);
            Response.StatusCode = result.Code;
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}