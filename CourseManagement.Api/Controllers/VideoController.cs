using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoController(IVideoService videoService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllVideos()
    {
        var videos = videoService.GetAllVideos();
        return Ok(videos);
    }

    [HttpGet("{id}")]
    public IActionResult GetVideoById(string id)
    {
        var video = videoService.GetById(id);
        return Ok(video);
    }

    [HttpPost]
    public IActionResult AddVideo([FromBody] VideoViewModel video)
    {
        var result = videoService.Create(video);
        return Ok(result);
    }

    [HttpPut]
    public IActionResult UpdateVideo([FromBody] VideoViewModel video)
    {
        var result = videoService.Update(video);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteVideo(string id)
    {
        var result = videoService.Delete(id);
        return Ok(result);
    }

    // [HttpGet]
    // public IActionResult GetVideoByLessonId(string id)
    // {
    //     var video = videoService.GetByLessonId(id);
    //     return Ok(video);
    // }

    [HttpGet("/lesson/{lessonId}")]
    public IActionResult GetAllVideosByLessonId(string lessonId)
    {
        var videos = videoService.GetAllVideosByLessonId(lessonId);
        return Ok(videos); 
    }

    [HttpPost("uploadVideo")]
    public IActionResult UploadAndCreateVideo(IFormFile videoFile, [FromBody]VideoViewModel model)
    {
        string savedUrl = string.Empty;
        var result = videoService.UploadAndCreateVideo(
            model,
            videoFile,
            Directory.GetCurrentDirectory(),
            url => savedUrl = url
        );
        return Ok();
    }
}