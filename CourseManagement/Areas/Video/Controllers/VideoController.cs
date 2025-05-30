using CourseManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Video.Controllers;

public class VideoController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public VideoController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetAllVideos()
    {
        var videos = _unitOfWork.VideoRepository.GetAll();
        return Json(new { data = videos });
    }
}