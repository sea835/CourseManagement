using CourseManagement.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.AdminSite.Areas.Video.Controllers;

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
        var videos = videoService.GetAllVideos().Data;
        return Json(new { data = videos });
    }
}