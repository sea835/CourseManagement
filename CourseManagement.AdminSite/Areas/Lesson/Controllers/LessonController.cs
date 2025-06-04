using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.AdminSite.Areas.Lesson.Controllers;

[Area("Lesson")]
public class LessonController: Controller
{
    private readonly ILessonService lessonService;
    
    public LessonController(ILessonService lessonService)
    {
        this.lessonService = lessonService;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetAllLessons()
    {
        var lessons = lessonService.GetAllLessons().Data;
        return Json(new { data = lessons });
    }
}