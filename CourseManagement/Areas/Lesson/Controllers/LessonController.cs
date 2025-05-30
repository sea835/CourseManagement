using CourseManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Lesson.Controllers;

[Area("Lesson")]
public class LessonController: Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public LessonController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetAllLessons()
    {
        var lessons = _unitOfWork.LessionRepository.GetAll();
        return Json(new { data = lessons });
    }
}