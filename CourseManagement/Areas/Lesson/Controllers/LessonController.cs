using CourseManagement.IServices;
using CourseManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Lesson.Controllers;

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
        var lessons = lessonService.GetAllLessons();
        return Json(new { data = lessons });
    }
}