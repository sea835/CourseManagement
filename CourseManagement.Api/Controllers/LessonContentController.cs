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
    public IActionResult GetLessonContentByLessonId(string lessonId)
    {
        var content = lessonContentService.GetContentByLessonId(lessonId);
        if (content == null) return NotFound();
        var result = new ContentResponseModel
        {
            ContentId = content.ContentId,
            LessonId = content.LessonId,
            MainContent = content.MainContent,
            Summary = content.Summary
        };
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetContentById(string id)
    {
        var c = lessonContentService.GetById(id);
        if (c == null) return NotFound();
        var content = new ContentResponseModel
        {
            ContentId = c.ContentId,
            LessonId = c.LessonId,
            MainContent = c.MainContent,
            Summary = c.Summary
        };
        return Ok(content);
    }

    [HttpPost]
    public ResultViewModel AddContent([FromBody] ContentRequestModel model)
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

    [HttpPut("{id}")]
    public ResultViewModel UpdateContent(string id, [FromBody] ContentRequestModel model)
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

    [HttpDelete("{id}")]
    public ResultViewModel DeleteContent(string id)
    {
        var result = lessonContentService.DeleteContent(id);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpPut("UpdateSummaryContent/{id}")]
    public ResultViewModel UpdateSummaryContent(string id, [FromBody] ContentRequestModel content)
    {
        var result = lessonContentService.UpdateSummary(id, content.Summary);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpPut("UpdateMainContent/{id}")]
    public ResultViewModel UpdateMainContent(string id, [FromBody] ContentRequestModel content)
    {
        var result = lessonContentService.UpdateMainContent(id, content.MainContent);
        Response.StatusCode = result.Code;
        return result;
    }
}