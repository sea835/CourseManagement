using CourseManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Chapter.Controllers;

[Area("Chapter")]
public class ChapterController: Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public ChapterController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetAllChapters()
    {
        var chapters = _unitOfWork.ChapterRepository.GetAll();
        return Json(new { data = chapters });
    }
}