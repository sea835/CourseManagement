using CourseManagement.Core.Models;
using System;
using System.Linq;
using CourseManagement.Core.RequestModels;
using CourseManagement.Core.ViewModels;
using CourseManagement.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace CourseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoController(IVideoService videoService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllVideos()
    {
        var videos = await videoService.GetAllVideos();
        var result = videos.Select(v => new VideoResponseModel
        {
            VideoId = v.VideoId,
            LessonId = v.LessonId,
            Title = v.Title,
            Url = v.Url,
            Duration = v.Duration,
            Provider = v.Provider,
            LessonTitle = v.LessonTitle,
            ChapterTitle = v.ChapterTitle,
            CourseTitle = v.CourseTitle
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVideoById(string id)
    {
        var v = await videoService.GetById(id);
        if (v == null) return NotFound();
        var video = new VideoResponseModel
        {
            VideoId = v.VideoId,
            LessonId = v.LessonId,
            Title = v.Title,
            Url = v.Url,
            Duration = v.Duration,
            Provider = v.Provider,
            LessonTitle = v.LessonTitle,
            ChapterTitle = v.ChapterTitle,
            CourseTitle = v.CourseTitle
        };
        return Ok(video);
    }

    [HttpPost]
    public async Task<IActionResult> AddVideo([FromBody] VideoRequestModel model)
    {
        var video = new VideoViewModel
        {
            VideoId = Guid.NewGuid().ToString(),
            LessonId = model.LessonId,
            Title = model.Title,
            Url = model.Url,
            Duration = model.Duration,
            Provider = model.Provider
        };
        await videoService.Create(video);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVideo(string id, [FromBody] VideoRequestModel model)
    {
        var video = new VideoViewModel
        {
            VideoId = id,
            LessonId = model.LessonId,
            Title = model.Title,
            Url = model.Url,
            Duration = model.Duration,
            Provider = model.Provider
        };
        await videoService.Update(video);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideo(string id)
    {
        await videoService.Delete(id);
        return Ok();
    }

    // [HttpGet]
    // public IActionResult GetVideoByLessonId(string id)
    // {
    //     var video = videoService.GetByLessonId(id);
    //     return Ok(video);
    // }

    [HttpGet("lesson/{lessonId}")]
    public IActionResult GetAllVideosByLessonId(string lessonId)
    {
        var videos = videoService.GetAllVideosByLessonId(lessonId)
            .Select(v => new VideoResponseModel
            {
                VideoId = v.VideoId,
                LessonId = v.LessonId,
                Title = v.Title,
                Url = v.Url,
                Duration = v.Duration,
                Provider = v.Provider,
                LessonTitle = v.LessonTitle,
                ChapterTitle = v.ChapterTitle,
                CourseTitle = v.CourseTitle
            });
        return Ok(videos);
    }

    [HttpPost("uploadVideo")]
    public async Task<IActionResult> UploadAndCreateVideo([FromForm]VideoRequestModel requestModel)
    {
        var model = new VideoViewModel()
        {
            LessonId = requestModel.LessonId,
            Title = requestModel.Title,
            Url = requestModel.Url,
            Duration = requestModel.Duration,
            Provider = requestModel.Provider,
            VideoFile = requestModel.VideoFile,
        };
        string savedUrl = string.Empty;
        await videoService.UploadAndCreateVideo(
            model,
            model.VideoFile,
            Directory.GetCurrentDirectory(),
            url => savedUrl = url
        );
        return Ok();
    }
}