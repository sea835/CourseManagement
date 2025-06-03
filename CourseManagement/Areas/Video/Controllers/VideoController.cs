using CourseManagement.IServices;
using CourseManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Video.Controllers;

[Area("Video")]
public class VideoController : Controller
{
    private readonly IVideoService videoService;

    public VideoController(IVideoService videoService)
    {
        this.videoService = videoService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetAllVideos()
    {
        var videos = videoService.GetAllVideos();
        return Json(new { data = videos });
    }
}