using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.ViewModels;
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
    
    public IActionResult GetLessonById(string id)
    {
        var lesson = lessonService.GetLessonById(id);
        if (lesson == null)
        {
            return NotFound();
        }
        return Json(new { data = lesson });
    }

    public IActionResult CreateLesson()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CreateLesson(LessonViewModel lesson)
    {
        var result = lessonService.CreateLesson(lesson);
        return RedirectToAction("Index");
    }

    public IActionResult EditLesson(string id)
    {
        var result = lessonService.GetLessonById(id);
        if (result == null)
        {
            return NotFound();
        }
        return View(result);
    }
    
    [HttpPost]
    public IActionResult EditLesson(LessonViewModel lesson)
    {
        var result = lessonService.UpdateLesson(lesson);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult DeleteLesson(string id)
    {
        var result = lessonService.DeleteLesson(id);
        return Json(new { success = true, message = "Lesson deleted successfully." });
    }
    
    public IActionResult ViewLessonContent(string id)
    {
        // var content = lessonService.GetContentByLessonId(id);
        return View();
    }
}