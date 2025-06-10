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
    public async Task<ResultViewModel> GetAllVideos()
    {
        try
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
            return ResultViewModel.Success("Get all videos successfully", result);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<ResultViewModel> GetVideoById(string id)
    {
        try
        {
            var v = await videoService.GetById(id);
            if (v == null)
            {
                return ResultViewModel.Fail("Video not found");
            }
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
            return ResultViewModel.Success("Get video by id successfully", video);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPost]
    public async Task<ResultViewModel> AddVideo([FromBody] VideoRequestModel model)
    {
        try
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
            return ResultViewModel.Success("Video created successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<ResultViewModel> UpdateVideo(string id, [FromBody] VideoRequestModel model)
    {
        try
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
            return ResultViewModel.Success("Video updated successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ResultViewModel> DeleteVideo(string id)
    {
        try
        {
            await videoService.Delete(id);
            return ResultViewModel.Success("Video deleted successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    // [HttpGet]
    // public IActionResult GetVideoByLessonId(string id)
    // {
    //     var video = videoService.GetByLessonId(id);
    //     return Ok(video);
    // }

    [HttpGet("lesson/{lessonId}")]
    public ResultViewModel GetAllVideosByLessonId(string lessonId)
    {
        try
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
            return ResultViewModel.Success("Get videos by lesson id successfully", videos);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPost("uploadVideo")]
    public async Task<ResultViewModel> UploadAndCreateVideo([FromForm]VideoRequestModel requestModel)
    {
        try
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
            return ResultViewModel.Success("Video uploaded successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}